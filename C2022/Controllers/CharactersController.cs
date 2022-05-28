using DBAccess.Models;
using DBAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace C2022.Controllers
{
        [Route("characters")]
        [ApiController]
    public class CharactersController : Controller
    {
        public IRepositories<Character> contextCustomers { get; private set; }
        public CharactersController(IRepositories<Character> _context)
        {
            contextCustomers = _context;

        }
        [HttpGet("browse")]
        public IActionResult BrowseCharacters()
        {
            List<Character> model = contextCustomers.GetAll();
            return View(model);
        }
        [HttpGet("add")]
        public IActionResult AddCharacter()
        {
            Character model = new Character { UserId = int.Parse(HttpContext.Session.GetString("Id")), FirstName = "", Strength = 0, Race = "", Intellect = 0, Charisma = 0, Wisdom = 0, LastName = "", Dexterity = 0, Bio = "", ImageLink = "" };
            return View(model);
        }
        [HttpGet("view/{id}")]
        public IActionResult CharacterProfile(int id)
        {
            Character model = contextCustomers.Get(id);
            return View(model);
        }
    }
}
