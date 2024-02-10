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

namespace CarRepair.Pages.Pages.Receptionist
{
    [Authorize(Roles ="Receptionist")]
    public class RejectedRepairs : PageModel
    {
        private readonly ILogger<RejectedRepairs> _logger;
        private readonly ApplicationDbContext _context;

        public List<Repair> Repairs { get; set; }

        public RejectedRepairs(ILogger<RejectedRepairs> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async void OnGet()
        {
            Repairs = await _context.Repairs
                            .Where(r => r.AppointmentStatus == AppointmentStatus.Rejected)
                            .Include(r => r.Issue)
                            .ToListAsync();
        }
    }
}