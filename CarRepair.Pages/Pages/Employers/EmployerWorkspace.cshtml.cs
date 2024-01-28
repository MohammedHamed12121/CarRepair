using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Data;
using CarRepair.Pages.Data.Migrations;
using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRepair.Pages.Pages.Employers
{
    public class EmployerWorkspace : PageModel
    {
        private readonly ILogger<EmployerWorkspace> _logger;
        private readonly ApplicationDbContext _context;
        public List<Acknowledge> Acknowledges { get; set; }

        public EmployerWorkspace(ILogger<EmployerWorkspace> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Acknowledges = _context.Acknowledges
                                    .Where(a=>a.CarStatus != Status.AppointmentAccepted)
                                    // .Include(a=>a.)
                                    .OrderBy(a=>a.VisitDate)
                                    .ToList();
        }
    }
}