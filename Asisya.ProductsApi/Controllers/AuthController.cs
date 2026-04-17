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