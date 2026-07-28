using ECommerceApi.DTOs;
using ECommerceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers
{
    /// <summary>
    /// Handles user authentication operations including account registration and user login sessions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        /// <param name="authService">The business logic service managing user authentication.</param>
        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        /// <summary>
        /// Registers a new user account within the system.
        /// </summary>
        [HttpPost("register")]
        public  async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await authService.Register(dto);
            return Ok(result);
        }

        /// <summary>
        /// Authenticates an existing user and provides a security token.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await authService.Login(dto);
            return Ok(new { token });
        }
    }
}
