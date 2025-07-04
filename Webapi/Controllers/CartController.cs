using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers
{
    public class CartController(ICartService cartService) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
        {
            var cart = await cartService.GetCartAsync(id);
            return cart ?? new ShoppingCart { Id = id };
        }


        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        {
            var updatedCart = await cartService.SetCartAsync(cart);

            if(updatedCart == null)
            {
                return BadRequest("Could not update the cart");
            }

            return updatedCart;
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteCart(string key)
        {
            var isCartDeleted = await cartService.DeleteCart(key);

            if (isCartDeleted)
            {
                return BadRequest("Could not delete the cart");
            }

            return Ok();
        }


    }
}
