using Microsoft.AspNetCore.Identity;

namespace TestOgSikkerhed.Data;

public class MyRoleHandler
{
    public async Task CreateUserRoles(string userEmail, string role, IServiceProvider provider)
    {
        var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();

        IdentityResult roleResult;

        var userRoleCheck = await roleManager.RoleExistsAsync(role);
        if (!userRoleCheck)
            roleResult = await roleManager.CreateAsync(new IdentityRole(role));

        IdentityUser identityUser = await userManager.FindByEmailAsync(userEmail);
        await userManager.AddToRoleAsync(identityUser, role);
    }
}
