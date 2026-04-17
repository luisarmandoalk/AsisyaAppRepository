using Asisya.Application.DTOs;
using Asisya.Domain.Entities;
using Asisya.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asisya.Api.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            product.ProductName = dto.ProductName;
            product.UnitPrice = dto.UnitPrice;
            product.CategoryId = dto.CategoryId;
            product.SupplierId = dto.SupplierId;

            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
         public IActionResult GetAll(

         int page = 1,
         int pageSize = 20,
         string search = "",
         decimal? minPrice = null,
         decimal? maxPrice = null)
            {
                var query = _context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                    query = query.Where(p => p.ProductName.Contains(search));

                if (minPrice.HasValue)
                    query = query.Where(p => p.UnitPrice >= minPrice.Value);

                if (maxPrice.HasValue)
                    query = query.Where(p => p.UnitPrice <= maxPrice.Value);

                var total = query.Count();

            var products = query
    .Include(p => p.Category)
    .OrderBy(p => p.ProductName)
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .Select(p => new
    {
        p.Id,
        p.ProductName,
        p.UnitPrice,
        CategoryName = p.Category.CategoryName,
        CategoryImageUrl = p.Category.Picture
    })
    .ToList();

            return Ok(new
                {
                    total,
                    page,
                    pageSize,
                    data = products
                });
            }
        // 
        // CREATE PRODUCT
        // 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = dto.ProductName,
                UnitPrice = dto.UnitPrice,
                CategoryId = dto.CategoryId,
                SupplierId = dto.SupplierId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                product.Id,
                product.ProductName,
                product.UnitPrice,
                product.CategoryId,
                product.SupplierId
            });
        }

        // =========================
        // GENERATE 100K PRODUCTS
        // =========================
        [HttpPost("generate")]
        public async Task<IActionResult> Generate(
            [FromQuery] Guid categoryId,
            [FromQuery] Guid supplierId)
        {
            const int total = 10;
            const int batchSize = 5000;

            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            try
            {
                for (int i = 1; i <= total; i += batchSize)
                {
                    var batch = new List<Product>(batchSize);

                    for (int j = 0; j < batchSize && (i + j) <= total; j++)
                    {
                        batch.Add(new Product
                        {
                            Id = Guid.NewGuid(),
                            ProductName = $"Product {i + j}",
                            UnitPrice = (i + j) % 1000,
                            CategoryId = categoryId,
                            SupplierId = supplierId
                        });
                    }

                    await _context.Products.AddRangeAsync(batch);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { inserted = total });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }
            finally
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        
        // BULK INSERT importar desde cvs
        // 
        [HttpPost("bulk")]
        public async Task<IActionResult> BulkInsert([FromBody] List<CreateProductDto> dtos)
        {
            var products = dtos.Select(dto => new Product
            {
                Id = Guid.NewGuid(),
                ProductName = dto.ProductName,
                UnitPrice = dto.UnitPrice,
                CategoryId = dto.CategoryId,
                SupplierId = dto.SupplierId
            }).ToList();

            const int batchSize = 5000;

            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            try
            {
                for (int i = 0; i < products.Count; i += batchSize)
                {
                    var batch = products.GetRange(i, Math.Min(batchSize, products.Count - i));

                    await _context.Products.AddRangeAsync(batch);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { inserted = products.Count });
            }
            finally
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }
    }
}