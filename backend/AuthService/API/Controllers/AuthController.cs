using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Models.RegisterRequest request)
        {
            var user = new User { UserName = request.Email, Email = request.Email, FullName = request.FullName };
            var token = await _authRepository.RegisterAsync(user, request.Password);

            if (token == null) return BadRequest("User registration failed");

            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authRepository.LoginAsync(request.Email, request.Password);

            if (token == null) return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}
