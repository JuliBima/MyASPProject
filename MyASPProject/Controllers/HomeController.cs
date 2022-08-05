using Microsoft.AspNetCore.Mvc;
using MyASPProject.Models;
using System.Diagnostics;

namespace MyASPProject.Controllers
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                HttpContext.Session.SetString("token", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Imp1bGliaW1hMTJAcmFwaWR0ZWNoLmlkIiwibmJmIjoxNjU5Njk0MDUxLCJleHAiOjE2NTk2OTc2NTEsImlhdCI6MTY1OTY5NDA1MX0.iYbZ3aAg6LZ610bRm57zAKO6qET-Xc0IUpv6xpapipg");
            }

            return View();
        }

        public IActionResult About()
        {
            string[] arrName = new string[] { "Erick", "Budi", "Bambang" };
            return new JsonResult(arrName);
        }

        public IActionResult Address()
        {
            return Content("Jl Mangga 12");
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