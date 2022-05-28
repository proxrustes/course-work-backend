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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace C2022.Controllers
{
    [Route("api/[controller]")]
    public class LoginApiController : Controller
    {
        private IConfiguration _config;
        public IRepositories<User> _context { get; private set; }
        public LoginApiController(IConfiguration config, IRepositories<User> context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("reg")]
        public IActionResult Register([FromForm] User user)
        {
            if (user != null)
            {
                user.Role = "user";
                _context.Add(user);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost("log")]
        public IActionResult Login([FromForm] UserLogin userLogin)
        {
            HttpClient client = new HttpClient();
            if (!ModelState.IsValid)
            {
                return View(userLogin);
            }

            var user = Authenticate(userLogin);
            if (user != null)
            {
                HttpContext.Session.SetString("Id", user.UserId.ToString());
                HttpContext.Session.SetString("Name", user.UserName);
                HttpContext.Session.SetString("Role", user.Role);

                return RedirectToAction("Index", "Home");
            }
            return NotFound("User not found");
        }
        [HttpGet("logOut")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        private User Authenticate(UserLogin userLogin)
        {
            List<User> getAll = _context.GetAll();
            User? currentUser = getAll.FirstOrDefault(x => x.UserName == userLogin.Username && x.Password == userLogin.Password);


            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}
