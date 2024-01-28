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
        public Acknowledge Acknowledge{get; set;}
        public int AppointmentId { get; set; }


        public SendConfirm(ILogger<SendConfirm> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet(int appointmentId)
        {
            AppointmentId = appointmentId;
        }

        public IActionResult OnPost()
        {   
            // Get the id of the appointment
            int.TryParse(Request.Form["AppointmentId"].ToString(), out int AppointmentId);
            // Get the appointment by the id
            var appointment = _context.Appointments.FirstOrDefault(a => a.Id == AppointmentId);
            // Then remove the appointment
            _context.Appointments.Remove(appointment);
            // Get the acknowledgement
            _context.Acknowledges.Add(Acknowledge);
            _context.SaveChanges();
            return RedirectToPage("DisplayAppointments");
            
        }
    }
}