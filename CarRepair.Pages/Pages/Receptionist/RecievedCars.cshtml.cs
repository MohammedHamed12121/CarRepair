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
    public class RecievedCars : PageModel
    {
        private readonly ILogger<RecievedCars> _logger;
        private readonly ApplicationDbContext _context;

        public List<Repair> Repairs { get; set; }


        public RecievedCars(ILogger<RecievedCars> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async void OnGet()
        {
            Repairs = await _context.Repairs
                            .Where(r => r.CarStatus >= Status.Recieved)
                            .Include(r=> r.Issue)
                            .ToListAsync();
        }
    }
}