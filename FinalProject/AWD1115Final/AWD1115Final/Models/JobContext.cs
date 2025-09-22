using Microsoft.EntityFrameworkCore;

namespace AWD1115Final.Models
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasKey(j => j.JobId);

            modelBuilder.Entity<Job>().HasData(
            new Job { JobId = 1, SkillLevel = 1, Name = "Oil Change", Description = "Change oil and oil filter", Priority = "Low", CompletionTime = 0.50f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 2, SkillLevel = 1, Name = "Breaks", Description = "Replace Break Pads", Priority = "Low", CompletionTime = 1f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 3, SkillLevel = 1, Name = "Air Filter", Description = "Replace Air Filter", Priority = "Low", CompletionTime = 0.25f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 4, SkillLevel = 1, Name = "Cabin Filter", Description = "Replace Cabin Filter", Priority = "Low", CompletionTime = 0.25f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 5, SkillLevel = 1, Name = "Tire Mounting And Balancing", Description = "Balance And Install/Mount New Tires", Priority = "Low", AssignedMechanic = "", CompletionTime = 2f, IsCompleted = true, CustomerName = "" },
                new Job { JobId = 6, SkillLevel = 1, Name = "Windshield Wipers", Description = "Replace Windshield Wipers", Priority = "Low", CompletionTime = 0.10f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 7, SkillLevel = 1, Name = "Tire Rotation", Description = "Rotate Tires", Priority = "Low", CompletionTime = 1f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 8, SkillLevel = 1, Name = "Change Fluids", Description = "Remove/Drain And Replace Vehicle's Fluids", Priority = "Low", CompletionTime = 0.50f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },

                new Job { JobId = 9, SkillLevel = 2, Name = "Catalitic Convertor", Description = "Replace Catalitic Convertor", Priority = "Low", CompletionTime = .50f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 10, SkillLevel = 2, Name = "Spark Plugs", Description = "Replace Of Spark Plugs", Priority = "Low", CompletionTime = .25f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 11, SkillLevel = 2, Name = "Internal Engine Work", Description = "Replace Head Gasket", Priority = "Low", CompletionTime = 10f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 12, SkillLevel = 2, Name = "Water Pump", Description = "Replace Water Pump", Priority = "Low", CompletionTime = .25f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 13, SkillLevel = 2, Name = "Alternator", Description = "Replace Alternator", Priority = "Low", CompletionTime = .25f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 14, SkillLevel = 2, Name = "Intake", Description = "Replace Intake", Priority = "Low", CompletionTime = .5f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 15, SkillLevel = 2, Name = "Exhaust Manifold", Description = "Replace Exhaust Manifold", Priority = "Low", CompletionTime = 1f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },

                new Job { JobId = 16, SkillLevel = 3, Name = "Clutch", Description = "Replace Clutch", Priority = "Low", CompletionTime = 6f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 17, SkillLevel = 3, Name = "Transmission", Description = "Replace Transmission", Priority = "Low", CompletionTime = 5f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 18, SkillLevel = 3, Name = "Electrical Issues", Description = "Fix Electrical Issues", Priority = "Low", CompletionTime = 3.25f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 19, SkillLevel = 3, Name = "Engine Internals", Description = "Replace Pistons And Rings", Priority = "Low", CompletionTime = 7.75f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },

                new Job { JobId = 20, SkillLevel = 4, Name = "Engine Replacement", Description = "Replace Engine", Priority = "Low", CompletionTime = 15f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 21, SkillLevel = 4, Name = "Fix Wiring", Description = "Fix Wiring", Priority = "Low", CompletionTime = 2.5f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 22, SkillLevel = 4, Name = "Work On A Maserati", Description = "Any Work Here", Priority = "Low", CompletionTime = 1.5f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" },
                new Job { JobId = 23, SkillLevel = 4, Name = "Work On A Alpha Romeo", Description = "Any Work Here", Priority = "Low", CompletionTime = 1.5f, AssignedMechanic = "", IsCompleted = true, CustomerName = "" }
            );
        }
    }
}