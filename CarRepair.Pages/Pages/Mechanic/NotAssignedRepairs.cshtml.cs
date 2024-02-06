using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Data;
using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRepair.Pages.Pages.Mechanic
{
    public class NotAssignedRepairs : PageModel
    {
        private readonly ILogger<NotAssignedRepairs> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public List<Repair> Repairs { get; set; }
        [BindProperty]
        public int RepairId { get; set; }

        public NotAssignedRepairs(ILogger<NotAssignedRepairs> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async void OnGet()
        {
            Repairs = await _context.Repairs
                            .Where(r => r.CarStatus == Status.Recieved)
                            .Include(r=> r.Issue)
                            .ToListAsync();
        }
        public async Task<IActionResult> OnPost()
        {
            var currentUserId = _userManager.GetUserId(User);

            // make the current user busy
            var currentUser = await _context.Users.Where(u => u.Id == currentUserId).FirstOrDefaultAsync();
            currentUser!.Busy = true;
            
            // assign the repair to be under repair
            var repair = await _context.Repairs
                                    .Where(r=> r.Id == RepairId)
                                    .FirstOrDefaultAsync();
            repair!.AssignedMechanicId = currentUserId;
            repair!.CarStatus = Status.UnderRepair;
            _context.Repairs.Update(repair);

            // save changes
            _context.SaveChanges();
            return RedirectToPage();
        }
    }
}