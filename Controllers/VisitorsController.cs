using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using VisitorManagementSystem.Data;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.ViewModels;

namespace VisitorManagementSystem.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IMapper _mapper { get; }

        public VisitorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Visitors
        public async Task<IActionResult> Index()
        {
            ViewBag.Welcome = "Welcome to the Visitor Management System";
            ViewBag.TodayDate = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            ViewBag.NewVisitor = new Visitors()
            {
                FirstName = "Howard",
                LastName = "Hughes",
            };

            var visitors = await _context.Visitors.Include(v => v.StaffNames).ToListAsync();
            var visitorsVM = _mapper.Map<IEnumerable<VisitorsVM>>(visitors);


            foreach (var v in visitorsVM)
            {
                v.FullName = v.FirstName + " " + v.LastName;
            }


            return View(visitorsVM);
        }

        // GET: Visitors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors
                .Include(v => v.StaffNames)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitors == null)
            {
                return NotFound();
            }

            var visitorsVM = _mapper.Map<VisitorsVM>(visitors);

            visitorsVM.FullName = visitorsVM.FirstName + " " + visitorsVM.LastName;
            return View(visitorsVM);
        }

        // GET: Visitors/Create
        public IActionResult Create()
        {
            ViewData["StaffNamesId"] = new SelectList(_context.StaffNames, "Id", "Name");

            VisitorsVM visitors = new VisitorsVM();
            visitors.DateIn = DateTime.Now;
            visitors.DateOut = DateTime.Now;

            return View(visitors);
        }

        // POST: Visitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Business,DateIn,DateOut,StaffNamesId")] Visitors visitors)
        {
            if (ModelState.IsValid)
            {
                visitors.Id = Guid.NewGuid();

                _context.Add(visitors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                var errors =
                    from value in ModelState.Values
                    where value.ValidationState == ModelValidationState.Invalid
                    select value;
                return View();  // <-- I set a breakpoint here, and examine "errors"
            }

            ViewData["StaffNamesId"] = new SelectList(_context.StaffNames, "Id", "Name", visitors.StaffNamesId);
            return View(visitors);
        }

        // GET: Visitors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors.FindAsync(id);

            VisitorsVM visitorsVM = new VisitorsVM();


            if (visitors == null)
            {
                return NotFound();
            }
            ViewData["StaffNamesId"] = new SelectList(_context.StaffNames, "Id", "Id", visitors.StaffNamesId);
            return View(visitors);
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,Business,DateIn,DateOut,StaffNamesId")] Visitors visitors)
        {
            if (id != visitors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitorsExists(visitors.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffNamesId"] = new SelectList(_context.StaffNames, "Id", "Id", visitors.StaffNamesId);
            return View(visitors);
        }

        // GET: Visitors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors
                .Include(v => v.StaffNames)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitors == null)
            {
                return NotFound();
            }

            return View(visitors);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Visitors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Visitors'  is null.");
            }
            var visitors = await _context.Visitors.FindAsync(id);
            if (visitors != null)
            {
                _context.Visitors.Remove(visitors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitorsExists(Guid id)
        {
            return (_context.Visitors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
