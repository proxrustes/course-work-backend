using DBAccess.Models;
using DBAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace C2022.Controllers
{
        [Route("users")]
        [ApiController]
    public class UserController : Controller
    {
        public IRepositories<User> contextCustomers { get; private set; }

        public UserController(IRepositories<User> _context)
        {
            contextCustomers = _context;

        }
        [HttpGet("browse")]
        public IActionResult BrowseAll()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                List<User> users = contextCustomers.GetAll();
                return View("BrowseUsers", users);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet("edit")]
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                User model = GetCurrent(int.Parse(HttpContext.Session.GetString("Id")));
                return View("EditProfile", model);
            }
            return RedirectToAction("Index", "Home");
        }
        public User? GetCurrent(int id)
        {
            return contextCustomers.Get(id);
        }
    }
}
