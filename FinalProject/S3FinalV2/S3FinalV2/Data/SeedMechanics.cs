using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class SeedMechanics
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        // Make sure role exists
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        if (!await roleManager.RoleExistsAsync("Mechanic"))
        {
            await roleManager.CreateAsync(new IdentityRole("Mechanic"));
        }

        var mechanicNames = new[]
        {
                "Ashtin Gebert",
                "Patrick Rodgers",
                "Aaron Barkley",
                "Bird Ball",
                "Kareem Ryan",
                "Gabriel Wilt",
                "Kelce Baker",
                "Stephinie John",
                "Beatriz Kareem",
                "Bill Patrick",
                "Ashtin Peterson",
                "Eric Newton",
                "Hanna Tyson",
                "Diya John",
                "Wilt Rodgers",
                "LeBron Ali",
                "Fatima Ball",
                "Aaron Magic"
            };

        foreach (var name in mechanicNames)
        {
            var email = name.Replace(" ", ".") + "@example.com";

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "Password123!");
            }

            if (!await userManager.IsInRoleAsync(user, "Mechanic"))
            {
                await userManager.AddToRoleAsync(user, "Mechanic");
            }
        }
    }
}