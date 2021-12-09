using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using NFTDemo.Models;

namespace NFTDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }*/
        //[HttpPost]   /*[FromBody] LoginParameter login*/
        [HttpPost]
        [Route("[action]")]
        public JsonResult Login(LoginParameter login)
        {
            if (login.Password != "Fuck") {
                return new JsonResult(new { message = "Login Failed" });
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("We were somewhere around Barstow, on the edge of the desert, when the drugs began to take hold"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                    new Claim(ClaimTypes.Role, "Administrator"),
            };

            var token = new JwtSecurityToken(null,
              null,
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new JsonResult(new { token = tokenString });
        }
    }
}
