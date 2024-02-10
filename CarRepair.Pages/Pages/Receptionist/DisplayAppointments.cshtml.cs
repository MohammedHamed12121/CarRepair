using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Data;
using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRepair.Pages.Pages.Appointments
{
    [Authorize(Roles ="Receptionist")]
    public class DisplayAppointments : PageModel
    {
        private readonly ILogger<DisplayAppointments> _logger;
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public List<Repair> Repairs {get; set;}
        public DisplayAppointments(ILogger<DisplayAppointments> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGet()
        {
            Repairs = await _context.Repairs
                            .Where(a => a.AppointmentStatus == AppointmentStatus.Sent)
                            .Include(a => a.User)
                            .Include(a => a.Issue)
                            .OrderBy(a => a.VisitDate)
                            .ToListAsync();
        }
    }
}