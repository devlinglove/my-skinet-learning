using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Webapi.RequestHelpers;

namespace Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _repo;

        public ProductsController(IGenericRepository<Product> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {

            var fullUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";

            var uri = new Uri(fullUrl);
            //var baseUri = uri.GetComponents(UriComponents.Scheme | UriComponents.Host | UriComponents.Port | UriComponents.Path, UriFormat.UriEscaped);

            var query = QueryHelpers.ParseQuery(uri.Query);
            //var queryPage = QueryHelpers.ParseQuery(uri.Query);
            var dictionary = query.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.FirstOrDefault() ?? string.Empty
            );

           

            var prodSpec = new ProductSpecification(specParams, dictionary);
            var products = await _repo.GetListWithSpec(prodSpec);
            var count = await _repo.CountAsync(prodSpec);

            var paginatedResult = new Pagination<Product>(count, products, specParams.PageSize, specParams.PageIndex);

            


            //var items = query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();












            return Ok(paginatedResult);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return NotFound();
            
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _repo.Add(product);

            if(await _repo.SaveAllChangesAsync())
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

            _repo.Update(product);

            if (await _repo.SaveAllChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem updating the product");

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return NotFound();

            _repo.Remove(product);

            if (await _repo.SaveAllChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem deleting the product");
        }

        //[HttpGet("brands")]
        //public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        //{
        //    // TODO - Implement method
        //    return Ok();
        //}

        //[HttpGet("types")]
        //public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        //{
        //    // TODO - Implement method
        //    return Ok();
        //}


        private bool ProductExists(int id)
        {
            return _repo.Exists(id);
        }

    }
}
