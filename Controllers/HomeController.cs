using AutoMapper;

using Microsoft.AspNetCore.Mvc;

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

        private IMapper _mapper { get; }
        private ApplicationDbContext _context { get; }
        private IDataSeeder _dataSeeder { get; }
        private ITextFileOperations _textFileOperations { get; }
        private ISweetAlert _sweetAlert { get; }
        private IDBCalls _dBCalls { get; }

        //constructor
        public HomeController(IMapper mapper, ApplicationDbContext context, IDataSeeder dataSeeder, ITextFileOperations textFileOperations, ILogger<HomeController> logger, ISweetAlert sweetAlert, IDBCalls dBCalls)
        {
            _mapper = mapper;
            _context = context;
            _dataSeeder = dataSeeder;
            _textFileOperations = textFileOperations;
            _logger = logger;
            _sweetAlert = sweetAlert;
            _dBCalls = dBCalls;
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
            //   TempData["noti"] = _sweetAlert.AlertPopupWithImage("The Awesome VMS", "Automate and record visitors to your organization", Enum.SweetAlertEnum.NotificationType.success);


            var visitors = await _dBCalls.VisitorsLoggedInAsync();
            var visitorsVM = _mapper.Map<IEnumerable<VisitorsVM>>(visitors);
            foreach (var v in visitorsVM)
            {
                v.FullName = v.FirstName + " " + v.LastName;
            }
            return View(visitorsVM);
        }



        [Route("/Home/Logout", Name = "LogoutRoute")]
        public async Task<IActionResult> LogOut(Guid? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors.FindAsync(id);

            if (visitors == null)
            {
                return NotFound();
            }
            //all we do is update the date out
            visitors.DateOut = DateTime.Now;

            _context.Update(visitors);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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