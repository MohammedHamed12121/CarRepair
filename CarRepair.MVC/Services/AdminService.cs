using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarRepair.MVC.Services
{
    public static class AdminService
    {
        public static async Task CreateAdminEmail(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string email = "admin@admin.com";
            string password = "Admin@123";

            if(await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser();
                user.UserName = email;
                user.Email = email;
                user.EmailConfirmed = true;

                await userManager.CreateAsync(user,password);

                await userManager.AddToRoleAsync(user, "Admin");
            }
        }   
    }
}