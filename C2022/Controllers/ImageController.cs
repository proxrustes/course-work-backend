using DBAccess.Models;
using DBAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace C2022.Controllers
{
    [Route("api/images")]
    [ApiController]
    [Authorize]
    public class ImageController : Controller
    {
        public IRepositories<Image> contextImages { get; private set; }

        public ImageController(IRepositories<Image> _context)
        {
            contextImages = _context;
        }
        [HttpGet("/getall/{id}")]
        public IEnumerable<Image> GetAll(int userId)
        {
            contextImages.Add(new Image { Link = "n" });
            return contextImages.GetAll();
        }
        [HttpGet("{id}")]
        public ActionResult<Image> Get(int id)
        {
            return contextImages.Get(id);
        }
        [HttpPost]
        public void Post([FromQuery] Image value)
        {
            contextImages.Add(value);
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromQuery] Image value)
        {
            contextImages.Update(value);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            contextImages.Delete(contextImages.Get(id));
        }
    }
}
