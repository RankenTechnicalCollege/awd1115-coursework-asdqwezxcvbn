using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using S3FinalV2.Models;

namespace S3FinalV2.Data
{
    public class MechTrackDbContext : IdentityDbContext
    {
        public MechTrackDbContext(DbContextOptions<MechTrackDbContext> options)
            : base(options)
        {
        }

        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<AssignedJobs> AssignedJobs { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Mechanics> Mechanics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Jobs>().HasData(
            new Jobs {JobId = 1, SkillLevel = 1, Name = "Oil Change", Description = "Change oil and oil filter", Priority = "Low" },
                new Jobs {JobId = 2, SkillLevel = 1, Name = "Breaks", Description = "Replace Break Pads", Priority = "Low" },
                new Jobs {JobId = 3, SkillLevel = 1, Name = "Air Filter", Description = "Replace Air Filter", Priority = "Low" },
                new Jobs {JobId = 4, SkillLevel = 1, Name = "Cabin Filter", Description = "Replace Cabin Filter", Priority = "Low" },
                new Jobs {JobId = 5, SkillLevel = 1, Name = "Tire Mounting And Balancing", Description = "Balance And Install/Mount New Tires", Priority = "Low"},
                new Jobs {JobId = 6, SkillLevel = 1, Name = "Windshield Wipers", Description = "Replace Windshield Wipers", Priority = "Low" },
                new Jobs {JobId = 7, SkillLevel = 1, Name = "Tire Rotation", Description = "Rotate Tires", Priority = "Low" },
                new Jobs {JobId = 8, SkillLevel = 1, Name = "Change Fluids", Description = "Remove/Drain And Replace Vehicle's Fluids", Priority = "Low" },

                new Jobs {JobId = 9, SkillLevel = 2, Name = "Catalitic Convertor", Description = "Replace Catalitic Convertor", Priority = "Low" },
                new Jobs {JobId = 10, SkillLevel = 2, Name = "Spark Plugs", Description = "Replace Of Spark Plugs", Priority = "Low" },
                new Jobs {JobId = 11, SkillLevel = 2, Name = "Internal Engine Work", Description = "Replace Head Gasket", Priority = "Low" },
                new Jobs {JobId = 12, SkillLevel = 2, Name = "Water Pump", Description = "Replace Water Pump", Priority = "Low" },
                new Jobs {JobId = 13, SkillLevel = 2, Name = "Alternator", Description = "Replace Alternator", Priority = "Low" },
                new Jobs {JobId = 14, SkillLevel = 2, Name = "Intake", Description = "Replace Intake", Priority = "Low  " },
                new Jobs {JobId = 15, SkillLevel = 2, Name = "Exhaust Manifold", Description = "Replace Exhaust Manifold", Priority = "Low"},

                new Jobs {JobId = 16, SkillLevel = 3, Name = "Clutch", Description = "Replace Clutch", Priority = "Low" },
                new Jobs {JobId = 17, SkillLevel = 3, Name = "Transmission", Description = "Replace Transmission", Priority = "Low" },
                new Jobs {JobId = 18, SkillLevel = 3, Name = "Electrical Issues", Description = "Fix Electrical Issues", Priority = "Low" },
                new Jobs {JobId = 19, SkillLevel = 3, Name = "Engine Internals", Description = "Replace Pistons And Rings", Priority = "Low" },

                new Jobs {JobId = 20, SkillLevel = 4, Name = "Engine Replacement", Description = "Replace Engine", Priority = "Low"},
                new Jobs {JobId = 21, SkillLevel = 4, Name = "Fix Wiring", Description = "Fix Wiring", Priority = "Low"},
                new Jobs {JobId = 22, SkillLevel = 4, Name = "Work On A Maserati", Description = "Any Work Here"},
                new Jobs {JobId = 23, SkillLevel = 4, Name = "Work On A Alpha Romeo", Description = "Any Work Here"}
            );

            builder.Entity<Mechanics>().HasData
            (
                new Mechanics { MechanicId = 1, Name = "Ashtin Gebert", SkillLevel = 4, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 2, Name = "Patrick Rodgers", SkillLevel = 4, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 3, Name = "Aaron Barkley", SkillLevel = 4, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 4, Name = "Bird Ball", SkillLevel = 3, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 5, Name = "Kareem Ryan", SkillLevel = 3, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 6, Name = "Gabriel Wilt", SkillLevel = 3, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 7, Name = "Kelce Baker", SkillLevel = 3, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 8, Name = "Stephinie John", SkillLevel = 2, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 9, Name = "Beatriz Kareem", SkillLevel = 2, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 10, Name = "Bill Patrick", SkillLevel = 2, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 11, Name = "Ashtin Peterson", SkillLevel = 2, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 12, Name = "Eric Newton", SkillLevel = 2, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 13, Name = "Hanna Tyson", SkillLevel = 1, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 14, Name = "Diya John", SkillLevel = 1, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 15, Name = "Wilt Rodgers", SkillLevel = 1, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 16, Name = "LeBron Ali", SkillLevel = 1, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 17, Name = "Fatima Ball", SkillLevel = 1, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } },
                new Mechanics { MechanicId = 18, Name = "Aaron Magic", SkillLevel = 1, AssignedJobs = new int[] { }, CompletedJobs = new int[] { } }
            );
        }
    }
}