using Microsoft.AspNetCore.Mvc;

namespace Drippyz.Controllers
{
    public class Store : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
