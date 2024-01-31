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

namespace CarRepair.Pages.Pages.Admin
{
    public class SendConfirm : PageModel
    {
        private readonly ILogger<SendConfirm> _logger;
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Repair Repair {get; set;}
        // public int RepairId { get; set; }
        // public DateOnly Visit { get; set; }


        public SendConfirm(ILogger<SendConfirm> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet(int appointmentId)
        {
            // RepairId = appointmentId;
            Repair = _context.Repairs
                            .Where(a=> a.Id == appointmentId)
                            .FirstOrDefault()!;
        }

        public IActionResult OnPost()
        {  
            Repair.AppointmentStatus = AppointmentStatus.Seen; 
            _context.Repairs.Update(Repair);
            _context.SaveChanges();
            return RedirectToPage("DisplayAppointments");
            
        }
    }
}