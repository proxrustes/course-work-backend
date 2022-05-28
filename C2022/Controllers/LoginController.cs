using DBAccess.Contexts;
using DBAccess.Models;
using DBAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        public IRepositories<User> _context  { get; private set; }

        public LoginController(IConfiguration config, IRepositories<User> context)
        {
            _config = config;
            _context = context;
        }
        [HttpGet("rd")]
        public IActionResult Redirection()
        {
           if (HttpContext.Session.GetString("Id")!=null)
            {
                User model = GetCurrent(int.Parse(HttpContext.Session.GetString("Id")));
                return View("ProfilePage", model);
            }
            return View("Index", "Login");

        }
        [HttpGet("index")]
        public IActionResult Index()
        {
            UserLogin userLogin = new UserLogin { Password = "", Username = "" };
            return View(userLogin);
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            UserLogin userLogin = new UserLogin { Password = "", Username = "" };
            return View(userLogin);
        }
        [HttpGet("register")]
        public IActionResult Register()
        {
            User User = new User { Password = "", UserName = "", Role = "" };
            return View(User);
        }
        public User? GetCurrent(int id)
        {
           return _context.Get(id);
        }
    }
}
