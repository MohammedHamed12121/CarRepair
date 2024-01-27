using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Data;
using CarRepair.Pages.Data.Migrations;
using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CarRepair.Pages.Pages.Appointments
{
    public class DisplayAcknowledgement : PageModel
    {
        private readonly ILogger<DisplayAcknowledgement> _logger;
        private readonly ApplicationDbContext _context;
        public List<Acknowledge> Acknowledges { get; set; }
        public Acknowledge acknowledge {get; set;}

        public DisplayAcknowledgement(ILogger<DisplayAcknowledgement> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Acknowledges = _context.Acknowledges.Where(a => a.Accept == false).ToList();
        }

        public IActionResult OnPost()
        {
            int.TryParse(Request.Form["AcknowledgeId"].ToString(), out int acknowledgeId);
            acknowledge = _context.Acknowledges
                            .Where(a => a.Id == acknowledgeId)
                            .FirstOrDefault()!;
            acknowledge.Accept = true;
            _context.Acknowledges.Update(acknowledge);
            _context.SaveChanges();
            return RedirectToPage("DisplayAcknowledgement");
        }
    }
}