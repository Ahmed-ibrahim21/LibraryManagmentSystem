using LibraryManagmentSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services
{
    public class RoleInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager; // Changed IdentityUser to User
        private readonly IConfiguration _configuration;

        public RoleInitializer(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task InitializeAsync()
        {
            await EnsureRoleExists("MEMBER");
            await EnsureRoleExists("LIBRARIAN");


            var userEmail = _configuration.GetValue<string>("adminAccount:Email");
            var userPassword = _configuration.GetValue<string>("adminAccount:Password");

            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userPassword))
            {
                throw new InvalidOperationException("Admin account email or password is not configured properly.");
            }

            await EnsureUserInRole(userEmail, "LIBRARIAN", userPassword);
        }

        private async Task EnsureRoleExists(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }
        }

        private async Task EnsureUserInRole(string userEmail, string roleName, string password)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                user = new User // Changed IdentityUser to User
                {
                    UserName = userEmail,
                    Email = userEmail
                };
                await _userManager.CreateAsync(user, password);
            }

            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}


//program implementation
//check role exist or not
//async Task EnsureRoleExists(RoleManager<IdentityRole> roleManager, string roleName)
//{
//    if (!await roleManager.RoleExistsAsync(roleName))
//    {
//        var role = new IdentityRole(roleName);
//        await roleManager.CreateAsync(role);
//    }
//}


//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    await EnsureRoleExists(roleManager, "MEMBER");
//    await EnsureRoleExists(roleManager, "Librarian");
//}
