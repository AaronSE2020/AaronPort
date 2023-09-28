using Microsoft.AspNetCore.Mvc;

namespace AaronPortfolio.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
