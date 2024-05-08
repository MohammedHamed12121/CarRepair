using System.Text.RegularExpressions;
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
            return View();
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
            
            // TODO: Add #of ingoing repairs & # of new appointments & #of finished repairs and use ajax
            return View(count);
        }


        public async Task<IActionResult> ServiceRequests(AppointmentStatus appointmentStatus, int page = 1, int pageSize = 10)
        {
            IQueryable<Repair> appointments = _context.Repairs
                                  .Where(r => r.AppointmentStatus == appointmentStatus);

            var pageCount = await appointments.CountAsync();

            appointments = appointments
                                  .Skip((page-1)*pageSize).Take(pageSize)
                                  .Include(r => r.User)
                                  .Include(r => r.Issue);

            var pager = new PagerVM()
            {
                Pager = new Pager(pageCount, page, pageSize),
                Controller = "Admin",
                Action = "ServiceRequests"
            };

            ViewBag.pager = pager;
            return View(await appointments.ToListAsync());
        }

        public IActionResult UserList()
        {
            // to get uses with no roles
            var users = _context.Users
                                .Where(c => !_context.UserRoles
                                                    .Select(b => b.UserId).Distinct()
                                                    .Contains(c.Id))
                                .ToList();
            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}