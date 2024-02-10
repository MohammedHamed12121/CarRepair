using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Data;
using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CarRepair.Pages.Pages.Receptionist
{
    [Authorize(Roles ="Receptionist")]
    public class SendRejection : PageModel
    {
        private readonly ILogger<SendRejection> _logger;
        private readonly ApplicationDbContext _context;


        [BindProperty]
        public Repair Repair {get; set;}
        public SendRejection(ILogger<SendRejection> logger, ApplicationDbContext context)
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
            Repair.AppointmentStatus = AppointmentStatus.Rejected; 
            _context.Repairs.Update(Repair);
            _context.SaveChanges();
            return RedirectToPage("DisplayAppointments");
            
        }
    }
}