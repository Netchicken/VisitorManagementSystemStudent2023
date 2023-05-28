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
        public ISweetAlert _sweetAlert { get; }

        //constructor
        public HomeController(IMapper mapper, ApplicationDbContext context, IDataSeeder dataSeeder, ITextFileOperations textFileOperations, ILogger<HomeController> logger, ISweetAlert sweetAlert)
        {
            _mapper = mapper;
            _context = context;
            _dataSeeder = dataSeeder;
            _textFileOperations = textFileOperations;
            _logger = logger;
            _sweetAlert = sweetAlert;
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
                // ViewData["Welcome"] = "Hello sucker";
            }


            ViewData["Conditions"] = _textFileOperations.LoadConditionsForAcceptanceText();

            //.Where(v => v.DateOut == null)
            var date = DateTime.Parse("1/1/0001");
            var visitors = await _context.Visitors.Where(v => v.DateOut == date).Include(v => v.StaffNames).ToListAsync();

            var visitorsVM = _mapper.Map<IEnumerable<VisitorsVM>>(visitors);


            foreach (var v in visitorsVM)
            {
                v.FullName = v.FirstName + " " + v.LastName;
            }
            TempData["noti"] = _sweetAlert.AlertPopupWithImage("The Awesome VMS", "Automate and record visitors to your organization", Enum.SweetAlertEnum.NotificationType.success);


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