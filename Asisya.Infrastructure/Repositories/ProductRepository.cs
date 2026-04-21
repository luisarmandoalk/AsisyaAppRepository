using Microsoft.EntityFrameworkCore;
using Asisya.Domain.Entities;
using Asisya.Domain.Interfaces;
using Asisya.Infrastructure.Persistence;

namespace Asisya.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll(int page, int pageSize, string search)
        {
            var query = _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.ProductName.Contains(search));

            return await query
                .AsNoTracking()
                .OrderBy(x => x.ProductName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Product?> GetById(Guid id)
        {
            return await _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task BulkInsert(List<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }
                
    }
}