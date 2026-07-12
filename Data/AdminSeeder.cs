using Microsoft.AspNetCore.Identity;

namespace TaskManagementSystem.Data;

public static class AdminSeeder
{
    public static async Task SeedAdminAsync(
        UserManager<IdentityUser> userManager)
    {
        const string email = "admin@task.com";
        const string password = "Admin@321";

        var adminUser = await userManager.FindByEmailAsync(email);

        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}