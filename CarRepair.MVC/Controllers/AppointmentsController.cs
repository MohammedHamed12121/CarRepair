using CarRepair.MVC.Data;
using CarRepair.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRepair.MVC.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AppointmentsController(ILogger<AppointmentsController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var curUserId = _userManager.GetUserId(User);
            var repairs = _context.Repairs
                              .Where(r => r.UserId == curUserId && r.AppointmentStatus == AppointmentStatus.NotSeen)
                              .Include(r => r.Issue)
                              .ToList();
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