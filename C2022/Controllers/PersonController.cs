using DBAccess.Contexts;
using DBAccess.Models;
using DBAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace C2022.Controllers
{
    [Route("api/characters")]
    [ApiController]
    [Authorize]
    public class PersonController : Controller
    {
        public IRepositories<Character> contextCharacters { get; private set; }

        public PersonController(IRepositories<Character> _context)
        {
            contextCharacters = _context;
        }
       
        [HttpGet]
        public IEnumerable<Character> Get()
        {
            return contextCharacters.GetAll();
        }
        [HttpGet("{id}")]
        public ActionResult<Character> Get(int id)
        {
            return contextCharacters.Get(id);
        }
        [HttpPost]
        public void Post([FromQuery] Character value)
        {
            contextCharacters.Add(value);
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromQuery] Character value)
        {
            contextCharacters.Update(value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            contextCharacters.Delete(contextCharacters.Get(id));
        }
    }
}
