using CarRepair.MVC.Data;
using CarRepair.MVC.Interfaces;
using CarRepair.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CarRepair.MVC.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemoryCache _memoryCache;
        private readonly IAppointmentRepository _appointmentRepo;

        public AppointmentsController(
                            ILogger<AppointmentsController> logger, 
                            UserManager<IdentityUser> userManager, 
                            IMemoryCache memoryCache, 
                            IAppointmentRepository appointmentRepo)
        {
            _logger = logger;
            _userManager = userManager;
            _memoryCache = memoryCache;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 9, AppointmentStatus appointmentStatus = AppointmentStatus.NotSeen)
        {
            var curUserId = _userManager.GetUserId(User);
            string dataCacheKey = $"Appointments_{appointmentStatus}_{page}_{pageSize}";
            
            var repairs = await  _appointmentRepo.GetAllAsync(
                    userId: curUserId!, 
                    dataCacheKey: dataCacheKey, 
                    page: page, 
                    pageSize: pageSize,
                    appointmentStatus
                    );
            
            var pageCount = await _appointmentRepo.GetAllItemsCountAsync(curUserId!, appointmentStatus);

            var pager = new Pager(pageCount, page, pageSize);
            ViewBag.pager = pager;

            return View(repairs);
        }

        [HttpGet]
        public IActionResult Send()
        {
            
            var repair = new Repair() ;
            return View(repair);
        }
        [HttpPost]
        public IActionResult Send(Repair repair)
        {
            var newRepair = repair;
            _appointmentRepo.Add(newRepair);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Accept([FromForm]int id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);

            appointment.AppointmentStatus = AppointmentStatus.Accept;

            await _appointmentRepo.Update(appointment);

            // TODO: Handle redirection
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int repairId)
        {
            var repair = await _appointmentRepo.GetByIdAsync(repairId);

            _appointmentRepo.Delete(repair);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}