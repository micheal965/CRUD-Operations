using CRUD.Data;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var records = _context.Set<Product>().ToList();
            return Ok(records);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var record = _context.Set<Product>().Find(id);
            return record == null ? NotFound() : Ok(record);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            _context.Set<Product>().Add(product);
            await _context.SaveChangesAsync();
            return Ok($"{product.Id} Created Successfully");
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            var existingproduct = _context.Set<Product>().Find(product.Id);
            existingproduct.Name = product.Name;
            existingproduct.Sku = product.Sku;
            _context.Set<Product>().Update(existingproduct);
            await _context.SaveChangesAsync();
            return Ok("Updated Successfully");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var existingproduct = _context.Set<Product>().Find(id);
            _context.Set<Product>().Remove(existingproduct);
            await _context.SaveChangesAsync();
            return Ok($"{existingproduct.Name} Deleted Successfully");
        }
    }
}
