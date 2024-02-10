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
    public class UserAcceptance : PageModel
    {
        private readonly ILogger<UserAcceptance> _logger;
        private readonly ApplicationDbContext _context;

        public List<Repair> Repairs { get; set; }

        [BindProperty]
        public int RepairId { get; set; }

        public UserAcceptance(ILogger<UserAcceptance> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async void OnGet()
        {
            Repairs = await _context.Repairs
                                    .Where(r=> r.AppointmentStatus == AppointmentStatus.Accept 
                                            && r.CarStatus == Status.AppointmentAccepted )
                                    .Include(r => r.Issue)
                                    .ToListAsync();
        }

        public IActionResult OnPost()
        {
            var repair = _context.Repairs
                                .Where(r=> r.Id == RepairId)
                                .FirstOrDefault();
            repair!.CarStatus = Status.Recieved;
            _context.Update(repair);
            _context.SaveChanges();
            return RedirectToPage("UserAcceptance");
        }
    }
}