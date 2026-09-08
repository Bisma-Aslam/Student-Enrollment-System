using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentEnrollmentBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,"Ali Jones"),
                new Claim(ClaimTypes.Role, "User")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my-super-secret-key-1234567890123456"));
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);
            
            var token=new JwtSecurityToken(
                claims:claims,
                expires:DateTime.UtcNow.AddMinutes(30),
                signingCredentials:credentials
            );
            return Ok(new { 
                token=new JwtSecurityTokenHandler().WriteToken(token)
            
            } );
        }



    }
}
