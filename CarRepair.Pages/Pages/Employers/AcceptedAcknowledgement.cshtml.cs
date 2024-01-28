using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Data;
using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CarRepair.Pages.Pages.Admin
{
    public class AcceptedAcknowledgement : PageModel
    {
        private readonly ILogger<AcceptedAcknowledgement> _logger;
        private readonly ApplicationDbContext _context;
        public List<Acknowledge> Acknowledges { get; set; }

        public AcceptedAcknowledgement(ILogger<AcceptedAcknowledgement> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            // get all the acknowledges order by date of the visit
            Acknowledges = _context.Acknowledges
                                    .Where(a => a.Accept == true && a.CarStatus == Status.AppointmentAccepted)
                                    .OrderBy(a => a.VisitDate)
                                    .ToList();
        }
    }
}