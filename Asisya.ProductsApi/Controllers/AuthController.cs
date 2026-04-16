using Asisya.Application.DTOs;
using Asisya.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;

namespace Asisya.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (dto.Username == "admin" && dto.Password == "123")
            {
                return Ok(new { token = "TOKEN_OK" });
            }

            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        public string User { get; set; }
    }
}