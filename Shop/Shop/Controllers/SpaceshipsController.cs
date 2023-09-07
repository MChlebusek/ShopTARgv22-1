using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class SpaceshipsController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
