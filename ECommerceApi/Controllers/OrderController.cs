using Asp.Versioning;
using ECommerceApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceApi.Controllers
{
    /// <summary>
    /// Manages the order processing and checkout system for authenticated users.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;

        /// <param name="service">The business logic service managing user orders and checkout pipelines.</param>
        public OrderController(OrderService service)
        {
            _service = service;
        }

        /// <summary>
        /// Converts the items in the authenticated user's shopping cart into a finalized purchase order.
        /// </summary>
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            return Ok(await _service.Checkout(userId));
        }

        /// <summary>
        /// Retrieves the entire order purchase history for the authenticated user.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            return Ok( await _service.GetUserOrders(userId));
        }
    }
}
