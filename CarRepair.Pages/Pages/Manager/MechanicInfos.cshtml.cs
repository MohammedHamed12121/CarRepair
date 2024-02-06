using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRepair.Pages.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRepair.Pages.Pages.Manager
{
    public class MechanicInfos : PageModel
    {
        private readonly ILogger<MechanicInfos> _logger;
        private readonly UserManager<AppUser> _userManager;
        public List<AppUser> Users { get; set; }

        public MechanicInfos(ILogger<MechanicInfos> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public void OnGet()
        {
            // TODO: Make the query get only the user with mechanic roles
            Users = _userManager.Users
                                .ToList();
        }
    }
}