using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using S3FinalV2.Data;
using S3FinalV2.Models;

public static class SeedMechanics
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var context = services.GetRequiredService<MechTrackDbContext>();

        if (!await roleManager.RoleExistsAsync("Mechanic"))
            await roleManager.CreateAsync(new IdentityRole("Mechanic"));

        var mechanicsData = new (string Name, int SkillLevel)[]
        {
            ("Ashtin Gebert", 4),
            ("Patrick Rodgers", 4),
            ("Aaron Barkley", 4),
            ("Bird Ball", 3),
            ("Kareem Ryan", 3),
            ("Gabriel Wilt", 3),
            ("Kelce Baker", 3),
            ("Stephinie John", 2),
            ("Beatriz Kareem", 2),
            ("Bill Patrick", 2),
            ("Ashtin Peterson", 2),
            ("Eric Newton", 2),
            ("Hanna Tyson", 1),
            ("Diya John", 1),
            ("Wilt Rodgers", 1),
            ("LeBron Ali", 1),
            ("Fatima Ball", 1),
            ("Aaron Magic", 1)
        };

        var userList = new List<IdentityUser>();

        foreach (var (name, _) in mechanicsData)
        {
            var email = name.Replace(" ", ".") + "@example.com";

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    Role = "Mechanic"
                };
                await userManager.CreateAsync(user, "Password123!");
            }

            if (!await userManager.IsInRoleAsync(user, "Mechanic"))
            {
                await userManager.AddToRoleAsync(user, "Mechanic");
            }

            userList.Add(user);
        }

        await context.SaveChangesAsync();

        for (int i = 0; i < mechanicsData.Length; i++)
        {
            var (name, skillLevel) = mechanicsData[i];
            var userId = userList[i].Id;

            if (!await context.Mechanics.AnyAsync(m => m.UserId == userId))
            {
                context.Mechanics.Add(new Mechanics
                {
                    Name = name,
                    SkillLevel = skillLevel,
                    UserId = userId
                });
            }
        }

        await context.SaveChangesAsync();
    }
}