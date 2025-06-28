using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            return Ok(await _repo.GetProductsAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _repo.AddProduct(product);

            if(await _repo.SaveChangesAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }

            return BadRequest("Problem creating the product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }

            _repo.UpdateProduct(product);

            if (await _repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating the product");

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            _repo.DeleteProduct(product);

            if (await _repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem deleting the product");
        }

        private bool ProductExists(int id)
        {
            return _repo.ProductExists(id);
        }

    }
}
