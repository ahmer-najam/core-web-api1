using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_web_api1.Context;
using core_web_api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace core_web_api1.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        [HttpPost, Route("Login")]
        public IActionResult Login([FromBody] Login login){

            if(login == null) return BadRequest("Invalid client request");

            if(login.UserName == "admin" && login.Password == "abc123"){
                var secretKey = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes("mySuperSecretKey@786"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new {Token = tokenString});
            }

            return Unauthorized("You are not authorized");
        }
    }

}