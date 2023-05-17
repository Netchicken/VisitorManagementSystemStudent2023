using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using VisitorManagementSystem.Models;

namespace VisitorManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<HomeController> _logger;

        //constructor
        public HomeController(IWebHostEnvironment _webHostEnvironment, ILogger<HomeController> logger)
        {
            webHostEnvironment = _webHostEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Welcome"] = "Oh No not you Again";

            string rootPath = webHostEnvironment.WebRootPath;

            FileInfo filePath = new FileInfo(Path.Combine(rootPath, ("CFA.txt")));

            string[] lines = System.IO.File.ReadAllLines(filePath.ToString());

            ViewData["Conditions"] = lines;

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