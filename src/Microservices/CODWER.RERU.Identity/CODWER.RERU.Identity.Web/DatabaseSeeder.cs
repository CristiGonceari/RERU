using System;
using System.Linq;
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

            if (userManager.Users.Any()) return;

            var user = new ERPIdentityUser()
            {
                Email = "admin.platforma@mail.com",
                UserName = "admin.platforma@mail.com",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEPl5AizO+7pAJaojbT4e1OlRoA0lvZkU/ohahAr8/4PWMGtUSK9zLw0cydtIQwk62A==",
                SecurityStamp = "Z53QFF4QIZWJANJ6GQUJD5KFHWJDAT4F",
                NormalizedEmail = "ADMIN.PLATFORMA@MAIL.COM",
                NormalizedUserName = "ADMIN.PLATFORMA@MAIL.COM",
                ConcurrencyStamp = "3002e66c-9a29-4af4-a622-2ace5e7cba90",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Name = "Admin",
                LastName = "Platforma",
                Avatar = null
            };

            appDbContext.Users.Add(user);
            appDbContext.SaveChanges();


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

            var userRole = new IdentityUserRole<string>();
            userRole.UserId = user.Id;
            userRole.RoleId = role1.Id;

            appDbContext.UserRoles.Add(userRole);
            appDbContext.SaveChanges();
        }
    }
}
