using AaronPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AaronPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Product() 
        {
            return View();
        }

        public IActionResult NasaPicture()
        {
            return View();
        }

        public IActionResult NasaPicture2()
        {
            return View();
        }

        public IActionResult FavoritesGallery()
        {
            return View();
        }

        public IActionResult AboutModel()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}