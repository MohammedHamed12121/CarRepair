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
    public class SentAppointments : PageModel
    {
        private readonly ILogger<SentAppointments> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public List<Repair> Repairs { get; set; }
        public SentAppointments(ILogger<SentAppointments> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async void OnGet()
        {
            var curUserId = _userManager.GetUserId(User);
            Repairs = await _context.Repairs
                              .Where(r => r.UserId == curUserId && r.AppointmentStatus == AppointmentStatus.Sent)
                              .Include(r => r.Issue)
                              .ToListAsync();
        }
    }
}