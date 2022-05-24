using Microsoft.AspNetCore.Mvc;

namespace SimpleEcommerce.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
