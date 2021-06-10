using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TravelService.Models;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Cors;

namespace TravelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TokenController : ControllerBase
    {
        private readonly travelContext _context;
        public IConfiguration _configuration;

        public TokenController(IConfiguration config, travelContext context)
        {
            _context = context;
            _configuration = config;
        }

        [HttpPost]
        public async Task<IActionResult> checkUser(Users userdata)
        {
            if(userdata != null && userdata.UserName != null && userdata.UserPass != null)
            {
                var user = await getUser(userdata.UserName, userdata.UserPass);
                if(user != null)
                {
                    Role role = getRole(user.RoleId);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.UserId.ToString()),
                        new Claim("Username", user.UserName),
                        new Claim(ClaimTypes.Role, role.RoleName),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                        claims, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), signin);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Not found");
                }
            } 
            else
            {
                return BadRequest("Invalid request!!!");
            }
        }

        private Role getRole(int? roleId)
        {
            return _context.Role.Find(roleId);
        }

        private async Task<Users> getUser(string username, string pass)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username && u.UserPass == pass);
        }

    }
}
