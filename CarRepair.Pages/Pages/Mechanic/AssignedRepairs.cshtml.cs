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
    public class AssignedRepairs : PageModel
    {
        private readonly ILogger<AssignedRepairs> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public List<Repair> Repairs { get; set; }
        [BindProperty]
        public int RepairId { get; set; }

        public AssignedRepairs(ILogger<AssignedRepairs> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async void OnGet()
        {
            Repairs = await _context.Repairs
                            .Where(r => r.CarStatus == Status.UnderRepair)
                            .Include(r=> r.Issue)
                            .ToListAsync();
        }
        public async Task<IActionResult> OnPost()
        {
            var currentUserId = _userManager.GetUserId(User);
            var repair = await _context.Repairs
                                    .Where(r=> r.Id == RepairId)
                                    .FirstOrDefaultAsync();
            // repair!.AssignedMechanicId = currentUserId;
            repair!.CarStatus = Status.Finished;
            _context.Repairs.Update(repair);
            _context.SaveChanges();
            return RedirectToPage();
        }
    }
}