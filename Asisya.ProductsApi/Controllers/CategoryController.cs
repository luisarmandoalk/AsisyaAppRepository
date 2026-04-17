using Asisya.Domain.Entities;
using Asisya.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Asisya.Api.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL (con paginación)
        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 20)
        {
            var total = _context.Categories.Count();

            var data = _context.Categories
                .OrderBy(c => c.CategoryName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new
            {
                total,
                page,
                pageSize,
                data
            });
        }

        // GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category model)
        {
            model.CategoryId = Guid.NewGuid();

            _context.Categories.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Category model)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
                return NotFound();

            category.CategoryName = model.CategoryName;
            category.Description = model.Description;
            category.Picture = model.Picture;

            await _context.SaveChangesAsync();

            return Ok(category);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}