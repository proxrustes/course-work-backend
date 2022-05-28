using Microsoft.AspNetCore.Mvc;

namespace C2022.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult BrowseImages()
        {
            return View();
        }
    }
}
