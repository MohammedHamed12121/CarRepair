using CarRepair.MVC.Data;
using CarRepair.MVC.Models;
using CarRepair.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRepair.MVC.Controllers
{
    [Authorize (Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var notSeenAppointments = _context.Repairs
                                  .Where(r => r.AppointmentStatus == AppointmentStatus.NotSeen)
                                  .Include(r => r.User)
                                  .Include(r => r.Issue)
                                  .ToList();

            var seenAppointments = _context.Repairs
                                  .Where(r => r.AppointmentStatus == AppointmentStatus.Seen)
                                  .Include(r => r.User)
                                  .Include(r => r.Issue)
                                  .ToList();

            var rejectedAppointments = _context.Repairs
                                  .Where(r => r.AppointmentStatus == AppointmentStatus.Rejected)
                                  .Include(r => r.User)
                                  .Include(r => r.Issue)
                                  .ToList();

            var acceptedAppointments = _context.Repairs
                                  .Where(r => r.AppointmentStatus == AppointmentStatus.Accept)
                                  .Include(r => r.User)
                                  .Include(r => r.Issue)
                                  .ToList();

            var indexVM = new AdminIndexVM(){
                Appointments = notSeenAppointments,
                Seen = seenAppointments,
                Rejects = rejectedAppointments,
                Accepted = acceptedAppointments
            };
            return View(indexVM);
        }

        public IActionResult RepairDetail(int id)
        {
            var repair = _context.Repairs
                                 .Where(r => r.Id == id)
                                 .Include(r => r.User)
                                 .Include(r => r.Issue)
                                 .FirstOrDefault();
            return View(repair);
        }

        public IActionResult AcceptOrRefuse(int id, bool isAccept = true)
        {
            var accept = new AcceptOrRefuseVM();
            accept.Id = id;
            accept.Accept = isAccept;
            return View(accept);
        }

        [HttpPost]
        public IActionResult AcceptOrRefuse(AcceptOrRefuseVM accept)
        {
            var repair = _context.Repairs
                                 .Where(r => r.Id == accept.Id)
                                 .FirstOrDefault();

            if(accept.Accept || accept.Price >= 0 )
            {
                repair.Price = accept.Price;
                repair.AppointmentStatus =  AppointmentStatus.Seen;
                repair.WorkerNote = accept.WorkerNote;
            }else{
                repair.WorkerNote = accept.WorkerNote;
                repair.AppointmentStatus =  AppointmentStatus.Rejected;
            }
            _context.Update(repair);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Dashboard()
        {
            var mechanics = await _userManager.GetUsersInRoleAsync("Mechanic");
            var count = mechanics.Count;
            return View(count);
        }
        public IActionResult MechanicList()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}