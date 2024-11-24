using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentGradings.API.Models.Responses;
using StudentGradings.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentGradings.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel user)
    {
        if (user is null)
        {
            return BadRequest("Invalid client request");
        }
        if (user.UserName == "johndoe" && user.Password == "def@123") // сверяем с бд запрос
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")); // константы
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
            issuer: "https://localhost:5001",// константы
            audience: "https://localhost:5001",// константы
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new AuthenticatedResponse { Token = tokenString });
        }
        return Unauthorized();
    }
}
