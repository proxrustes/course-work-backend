using DBAccess.Models;
using DBAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace C2022.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        public IRepositories<User> contextCustomers { get; private set; }

        public UserController(IRepositories<User> _context)
        {
            contextCustomers = _context;
        }

        [HttpGet("AuthTest")]
        [Authorize]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.UserName}, you are an little piece of shit");
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return contextCustomers.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            
            return contextCustomers.Get(id);
        }
        
        [HttpPost]
        public void Post([FromQuery] User value)
        {
            contextCustomers.Add(value);
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromQuery] User value)
        {
            contextCustomers.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            contextCustomers.Delete(contextCustomers.Get(id));
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
