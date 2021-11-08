using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVU.ERP.Identity.Context;
using CVU.ERP.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace CODWER.RERU.Identity.Web
{
    public static class DbSeeder
    {
        public static void SeedDb(IServiceProvider serviceProvider)
        {
            var userManager = (UserManager<ERPIdentityUser>)serviceProvider.GetService(typeof(UserManager<ERPIdentityUser>));
            var appDbContext = (IdentityDbContext)serviceProvider.GetService(typeof(IdentityDbContext));

            if (!userManager.Users.Any())
            {

                var User = new ERPIdentityUser()
                {
                    Email = "admin@mail.com",
                    UserName = "admin.platforma@mail.com",
                    EmailConfirmed = true
                };

                if (!appDbContext.Roles.Any())
                {
                    var role1 = new IdentityRole()
                    {
                        Name = "Administrator",
                        NormalizedName = "Administrator",
                        Id = "ebb75d4b-6f54-4321-bda9-f6897e8b0d9d"
                    };

                    var role2 = new IdentityRole()
                    {
                        Name = "Member",
                        NormalizedName = "Member",
                        Id = "827e2a90-a645-4a82-95fa-7b585b8dc744"
                    };

                    appDbContext.Roles.AddRange(role1, role2);
                    appDbContext.SaveChanges();
                }

                userManager.CreateAsync(User, "Parola11a#").Wait();

                userManager.AddToRoleAsync(User, "Administrator").Wait();
            }
        }
    }
}
