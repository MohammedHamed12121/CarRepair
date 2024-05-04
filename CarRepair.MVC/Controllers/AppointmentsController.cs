using CarRepair.MVC.Data;
using CarRepair.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CarRepair.MVC.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemoryCache _memoryCache;

        public AppointmentsController(ILogger<AppointmentsController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, IMemoryCache memoryCache = null)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 9)
        {
            var curUserId = _userManager.GetUserId(User);
            string dataCacheKey = $"Appointments_{page}_{pageSize}";
            string countCacheKey = "Count";

            if(!_memoryCache.TryGetValue(dataCacheKey, out List<Repair> repairs) || 
                !_memoryCache.TryGetValue(countCacheKey, out int pageCount)
            )
            {
                var pageSkip = (page - 1) * pageSize;

                repairs = await _context.Repairs
                              .Where(r => r.UserId == curUserId && r.AppointmentStatus == AppointmentStatus.NotSeen)
                              .Include(r => r.Issue)
                              .Skip(pageSkip).Take(pageSize)
                              .ToListAsync();
                pageCount = _context.Repairs.Count();
                              

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _memoryCache.Set(dataCacheKey, repairs, cacheExpiryOptions);
                _memoryCache.Set(countCacheKey, pageCount, cacheExpiryOptions);
            }

            var pager = new Pager(pageCount, page, pageSize);
            ViewBag.pager = pager;

            return View(repairs);
        }

        [HttpGet]
        public IActionResult MakeAppointment()
        {
            
            var repair = new Repair() ;
            return View(repair);
        }
        [HttpPost]
        public IActionResult MakeAppointment(Repair repair)
        {
            var newRepair = repair;
            _context.Repairs.Add(newRepair);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Reply()
        {
            var curUserId = _userManager.GetUserId(User);
            var repair = _context.Repairs
                                 .Where( r => r.UserId == curUserId && r.AppointmentStatus == AppointmentStatus.Seen)
                                 .Include(r => r.Issue)
                                 .ToList();
            return View(repair);
        }

        [HttpPost]
        public IActionResult Reply(int id)
        {
            var appointment = _context.Repairs
                                      .Where(r => r.Id == id)
                                      .FirstOrDefault();

            appointment.AppointmentStatus = AppointmentStatus.Accept;
            _context.Repairs.Update(appointment);
            _context.SaveChanges();
            return RedirectToAction("Reply");
        }

        public IActionResult Rejected()
        {
            var curUserId = _userManager.GetUserId(User);
            var repair = _context.Repairs
                                 .Where(r => r.UserId == curUserId && r.AppointmentStatus == AppointmentStatus.Rejected)
                                 .Include(r => r.Issue)
                                 .ToList();
            return View(repair);
        }
        public IActionResult Accepted()
        {
            var curUserId = _userManager.GetUserId(User);
            var repair = _context.Repairs
                                 .Where(r => r.UserId == curUserId && r.AppointmentStatus == AppointmentStatus.Accept)
                                 .Include(r => r.Issue)
                                 .ToList();
            return View(repair);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}