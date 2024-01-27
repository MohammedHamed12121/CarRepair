using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Data;
using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRepair.Pages.Pages.Appointments
{
    public class DisplayAppointments : PageModel
    {
        private readonly ILogger<DisplayAppointments> _logger;
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public List<Appointment> appointments {get; set;}
        public DisplayAppointments(ILogger<DisplayAppointments> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            appointments = _context.Appointments
                            .Include(a => a.Issue)
                            .ToList();
        }
    }
}