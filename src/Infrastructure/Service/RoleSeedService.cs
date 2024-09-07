using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Service;

public class RoleSeedService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleSeedService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedRolesAsync()
    {
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }
    }
}
