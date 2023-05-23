using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

using VisitorManagementSystem.Data;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Services;
using VisitorManagementSystem.ViewModels;

namespace VisitorManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public IMapper _mapper { get; }
        public ApplicationDbContext _context { get; }
        public IDataSeeder _dataSeeder { get; }
        private ITextFileOperations _textFileOperations { get; }

        //constructor
        public HomeController(IMapper mapper, ApplicationDbContext context, IDataSeeder dataSeeder, ITextFileOperations textFileOperations, ILogger<HomeController> logger)
        {
            _mapper = mapper;
            _context = context;
            _dataSeeder = dataSeeder;
            _textFileOperations = textFileOperations;
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
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

            var visitors = await _context.Visitors.Include(v => v.StaffNames).ToListAsync();
            var visitorsVM = _mapper.Map<IEnumerable<VisitorsVM>>(visitors);


            foreach (var v in visitorsVM)
            {
                v.FullName = v.FirstName + " " + v.LastName;
            }


            return View(visitorsVM);

            //   return View();
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