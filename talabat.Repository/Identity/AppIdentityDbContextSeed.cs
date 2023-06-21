using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entities.Identity;

namespace talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {

                var user = new AppUser()
                {
                    DisplayName = "Ahmed Nasr",
                    Email = "ahmed.nasr@linkdev.com",
                    UserName = "ahmed.nasr",
                    PhoneNumber = "01122334455"
                };
                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
