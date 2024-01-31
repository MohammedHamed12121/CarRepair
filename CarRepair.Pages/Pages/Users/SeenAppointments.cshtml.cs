using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Data;
using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRepair.Pages.Pages.Users
{
    public class SeenAppointments : PageModel
    {
        private readonly ILogger<SeenAppointments> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public List<Repair> Repairs { get; set; }
        [BindProperty]
        public int RepairId { get; set; }

        public SeenAppointments(ILogger<SeenAppointments> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async void OnGet()
        {
            var curUserId = _userManager.GetUserId(User);
            Repairs = await _context.Repairs
                              .Where(r => r.UserId == curUserId && r.AppointmentStatus == AppointmentStatus.Seen)
                              .Include(r => r.Issue)
                              .ToListAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            var repair = await _context.Repairs.FirstOrDefaultAsync(r => r.Id == RepairId);
            repair.AppointmentStatus = AppointmentStatus.Accept;
            _context.Repairs.Update(repair);
            _context.SaveChanges();
            // return RedirectToPage("DisplayAcknowledgement");
            return Page();
        }
    }
}