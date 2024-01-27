using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Data;
using CarRepair.Pages.Models;
using CarRepair.Pages.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CarRepair.Pages.Pages.Appointments
{
    public class MakeAppointment : PageModel
    {
        private readonly ILogger<MakeAppointment> _logger;
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Appointment Appointment {get; set;}

        public MakeAppointment(ILogger<MakeAppointment> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

        }
        public void OnPost()
        {
            string value = $"{Appointment.Id} - {Appointment.VisitTime}";
            _context.Appointments.Add(Appointment);
            _context.SaveChanges();
        }
    }
}