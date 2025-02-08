using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Recrutamento.API.DTOs.Auth;
using Recrutamento.API.Services;
using Recrutamento.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Recrutamento.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<User> _userManager,
                                IConfiguration _configuration,
                                JwtTokenService _jwtTokenService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email.Trim());
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = _jwtTokenService.GenerateJwtToken(user);
                return Ok(new { token });
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("validate")]
        public IActionResult ValidateToken()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidAudience = _configuration["Jwt:Issuer"],
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    return Ok(new { valid = true });
                }
                catch
                {
                    return BadRequest(new { valid = false });
                }
            }
            return BadRequest(new { valid = false, message = "Token is missing or invalid" });
        }
    }
}
