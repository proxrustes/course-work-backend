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
    public class CharactersApiController : Controller
    {
        public IRepositories<Character> contextCharacters { get; private set; }

        public CharactersApiController(IRepositories<Character> _context)
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
        public void Add([FromForm] Character value)
        {
            contextCharacters.Add(value);
        }
        [HttpPut("{id}")]
        public void Put([FromForm] Character value)
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
