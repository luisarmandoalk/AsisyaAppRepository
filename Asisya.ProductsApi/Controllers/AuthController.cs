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

		public AuthController(JwtService jwtService) { _jwtService = jwtService; }
		[HttpPost("login")]

		[HttpPost("login")]
		public IActionResult Login([FromBody] LoginDto dto)
		{
			if (dto.Username == "admin" && dto.Password == "123")
			{
				var token = _jwtService.GenerateToken(dto.Username);

				return Ok(new { token });
			}

			return Unauthorized();
		}
	}
}
