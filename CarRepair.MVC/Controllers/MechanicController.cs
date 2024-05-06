using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.MVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRepair.MVC.Controllers
{
    public class MechanicController : Controller
    {
        private readonly ILogger<MechanicController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public MechanicController(
            ILogger<MechanicController> logger, 
            UserManager<IdentityUser> userManager, 
            ApplicationDbContext context
        )
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var mechanics = await _userManager.GetUsersInRoleAsync("Mechanic");
            return View(mechanics);
        }


        [HttpPost]
        public async Task<IActionResult> Add(
            [FromForm] string username,
            [FromForm] string email,
            [FromForm] string password
        )
        {
            if (await _userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser()
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, password);
                await _userManager.AddToRoleAsync(user, "Mechanic");
                
            }
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}