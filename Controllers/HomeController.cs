using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using VisitorManagementSystem.Models;
using VisitorManagementSystem.Services;

namespace VisitorManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public IDataSeeder _dataSeeder { get; }
        private ITextFileOperations _textFileOperations { get; }

        //constructor
        public HomeController(IDataSeeder dataSeeder, ITextFileOperations textFileOperations, ILogger<HomeController> logger)
        {
            _dataSeeder = dataSeeder;
            _textFileOperations = textFileOperations;
            _logger = logger;
        }

        public IActionResult Index()
        {

            //run the dataseeder
            _dataSeeder.SeedAsync().Wait();

            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                int placeholder = User.Identity.Name.IndexOf("@");
                ViewData["Welcome"] = "Oh No not you Again - " + User.Identity.Name.Substring(0, placeholder);
            }
            else
            {
                ViewData["Welcome"] = "Hello sucker";
            }


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