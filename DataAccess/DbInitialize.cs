using Core.Constants;
using Core.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbInitialize
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration _configuration)
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString()
                    });
                }
            }

            if (await userManager.FindByNameAsync("admin") == null)
            {
                var user = new User
                {
                    UserName = "admin",
                    Fullname = "Admin Adminov",
                    Email = "mirgubad@gmail.com"

                };
                user.EmailConfirmed = true;
                var result = await userManager.CreateAsync(user, "Admin1234*");
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description.ToString());
                    }
                }

                await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            }
        }
    }
}
