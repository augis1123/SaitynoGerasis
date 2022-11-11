using Microsoft.AspNetCore.Identity;
using SaitynoGerasis.Data.Entities;

namespace SaitynoGerasis.Data
{
    public class AuthDbSeeder
    {
        private readonly UserManager<naudotojas> userManager1;
        private readonly RoleManager<IdentityRole> roleManager1;
        public AuthDbSeeder(UserManager<naudotojas> userManager, RoleManager<IdentityRole> role)
        {
            userManager1 = userManager;
            roleManager1 = role;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddAdminUser();
        }
        private async Task AddAdminUser()
        {
            var admin = new naudotojas
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var exist = await userManager1.FindByNameAsync(admin.UserName);
            if (exist == null)
            {
                var createAdmin = await userManager1.CreateAsync(admin, "12345");
                if (createAdmin.Succeeded)
                {
                    await userManager1.AddToRolesAsync(admin, Roles.All);
                }
            }
        }
        private async Task AddDefaultRoles()
        {
            foreach (var item in Roles.All)
            {
                var roleExist = await roleManager1.RoleExistsAsync(item);
                if (!roleExist)
                {
                    await roleManager1.CreateAsync(new IdentityRole(item));
                }
            }
        }

    }
}
