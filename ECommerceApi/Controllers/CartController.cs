using Asp.Versioning;
using ECommerceApi.DTOs;
using ECommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceApi.Controllers
{
    /// <summary>
    /// Manages the shopping cart operations for authenticated users.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        /// <param name="service">The business logic service managing user shopping carts.</param>
        public CartController(CartService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adds a product item to the authenticated user's shopping cart.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var result = await _service.AddToCart(userId, dto);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves the entire shopping cart content for the authenticated user.
        /// </summary>
        [HttpGet]   
        public async Task<IActionResult> GetCart()
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            return Ok(await _service.GetUserCart(userId));
        }

        /// <summary>
        /// Removes a specific item from the authenticated user's shopping cart.
        /// </summary>
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            return Ok( await _service.RemoveFromCart(userId, cartItemId));
        }
    }
}
