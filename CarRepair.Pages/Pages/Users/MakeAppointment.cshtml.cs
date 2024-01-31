using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Data;
using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CarRepair.Pages.Pages.Appointments
{
    public class MakeAppointment : PageModel
    {
        private readonly ILogger<MakeAppointment> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public Repair Repair {get; set;}

        [BindProperty(SupportsGet = true)]
        public string CurrentUserId { get; set; }

        public MakeAppointment(ILogger<MakeAppointment> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public void OnGet()
        {
            CurrentUserId = _userManager.GetUserId(User);
        }
        public IActionResult OnPost()
        {
            Repair.UserId = CurrentUserId;
            _context.Repairs.Add(Repair);
            _context.SaveChanges();
            return RedirectToPage("User");
        }
    }
}