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

namespace CarRepair.Pages.Pages.Receptionist
{
    public class AcceptedRepairs : PageModel
    {
        private readonly ILogger<AcceptedRepairs> _logger;
        private readonly ApplicationDbContext _context;

        public List<Repair> Repairs { get; set; }
        public AcceptedRepairs(ILogger<AcceptedRepairs> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async void OnGet()
        {
            Repairs = await _context.Repairs
                            .Where(r => r.AppointmentStatus == AppointmentStatus.Seen)
                            .Include(r => r.Issue)
                            .ToListAsync();
        }
    }
}