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

namespace CarRepair.Pages.Pages.Manager
{
    public class Repairs : PageModel
    {
        private readonly ILogger<Repairs> _logger;
        private readonly ApplicationDbContext _context;
        public List<Repair> NotStartedRepairs { get; set; }

        public Repairs(ILogger<Repairs> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            NotStartedRepairs = _context.Repairs
                                // .Where(r => r.CarStatus < Status.UnderRepair && r.AppointmentStatus == AppointmentStatus.Accept)
                                .Where(r => r.CarStatus >= Status.Recieved)
                                .Include(r => r.Issue)
                                .ToList();
        }
    }
}