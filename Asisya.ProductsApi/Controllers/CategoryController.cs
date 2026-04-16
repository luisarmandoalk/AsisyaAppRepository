using Microsoft.AspNetCore.Mvc;
using Asisya.Domain.Entities;
using Asisya.Infrastructure.Persistence;

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

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            category.CategoryId = Guid.NewGuid();

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }
    }
}