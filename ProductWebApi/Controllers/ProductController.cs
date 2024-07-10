using Microsoft.AspNetCore.Mvc;

using ProductWebApi.Data;
using ProductWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _productDbContext;

        public ProductController(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProduct()
        {
            return _productDbContext.Products;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productDbContext.Products.FindAsync(id);
            return product;
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            await _productDbContext.Products.AddAsync(product);
            await _productDbContext.SaveChangesAsync();

            return Ok();
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public async Task<ActionResult> Update(Product product)
        {
            _productDbContext.Products.Update(product);
            await _productDbContext.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productDbContext.Products.FindAsync(id);
            _productDbContext.Products.Remove(product);
            await _productDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
