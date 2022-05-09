using DBAccess.Contexts;
using DBAccess.Models;
using DBAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace C2022.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public IRepositories<User> _context  { get; private set; }


        public LoginController(IConfiguration config, IRepositories<User> context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(new UserLogin { Username = "zhenia", Password = "sendnudes"});
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }
        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        private User Authenticate(UserLogin userLogin)
        {
            _context.Add(new User { UserName = "zhenia", Password = "sendnudes", Role = "admin" });
            IEnumerable<User> getAll = _context.GetAll();
            User? currentUser = getAll.FirstOrDefault(x => x.UserName == userLogin.Username && x.Password == userLogin.Password);
               

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
