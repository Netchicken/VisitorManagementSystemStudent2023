using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using VisitorManagementSystem.Models;
using VisitorManagementSystem.Services;

namespace VisitorManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public ITextFileOperations _textFileOperations { get; }

        //constructor
        public HomeController(ITextFileOperations textFileOperations, ILogger<HomeController> logger)
        {
            _textFileOperations = textFileOperations;
            _logger = logger;
        }

        public IActionResult Index()
        {



            ViewData["Welcome"] = "Oh No not you Again";



            ViewData["Conditions"] = _textFileOperations.LoadConditionsForAcceptanceText();

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