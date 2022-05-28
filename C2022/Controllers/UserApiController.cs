using DBAccess.Models;
using DBAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace C2022.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserApiController : Controller
    {
        public IRepositories<User> contextCustomers { get; private set; }

        public UserApiController(IRepositories<User> _context)
        {
            contextCustomers = _context;

        }

        [HttpGet("AuthTest")]
        public IActionResult AdminsEndpoint()
        {
            var identity = HttpContext.Session.GetString("Name");
            return Ok("Hello " + identity);
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return contextCustomers.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            return contextCustomers.Get(id);
        }
        
        [HttpPost]
        public IActionResult Post([FromForm] User value)
        {
            contextCustomers.Update(value);
            return RedirectToAction("Redirection", "Login");
        }
        
        [HttpPost("{id}")]
        public IActionResult Put([FromForm] User value)
        {
            contextCustomers.Update(value);
            return RedirectToAction("Redirection", "Login");
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            contextCustomers.Delete(contextCustomers.Get(id));
        }

    }
}
