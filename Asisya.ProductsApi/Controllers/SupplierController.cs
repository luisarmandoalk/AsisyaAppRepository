using Asisya.Domain.Entities;
using Asisya.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Asisya.Api.Controllers
{
    [ApiController]
    [Route("api/supplier")]
    public class SupplierController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SupplierController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE SUPPLIER
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Supplier supplier)
        {
            supplier.SupplierId = Guid.NewGuid();

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return Ok(supplier);
        }

        // GET ALL SUPPLIERS
        [HttpGet]
        public IActionResult GetAll()
        {
            var suppliers = _context.Suppliers.ToList();
            return Ok(suppliers);
        }

        //  GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(x => x.SupplierId == id);

            if (supplier == null)
                return NotFound();

            return Ok(supplier);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
                return NotFound();

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted successfully" });
        }
    }
}