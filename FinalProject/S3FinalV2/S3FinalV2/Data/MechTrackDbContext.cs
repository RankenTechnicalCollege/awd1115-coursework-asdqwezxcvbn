using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using S3FinalV2.Models;

namespace S3FinalV2.Data
{
    public class MechTrackDbContext : IdentityDbContext<ApplicationUser>
    {
        public MechTrackDbContext(DbContextOptions<MechTrackDbContext> options)
            : base(options) { }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Mechanics> Mechanics { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<AssignedJobs> AssignedJobs { get; set; }
        public DbSet<MechanicAssignment> MechanicAssignments { get; set; }
        public DbSet<WorkWeek> WorkWeeks { get; set; }
        public DbSet<WorkWeekAssignment> WorkWeekAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customers>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Mechanics>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AssignedJobs>()
                .HasOne(a => a.Jobs)
                .WithMany()
                .HasForeignKey(a => a.JobsId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AssignedJobs>()
                .HasOne(a => a.Customer)
                .WithMany()
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MechanicAssignment>()
                .HasOne(ma => ma.AssignedJob)
                .WithMany(a => a.MechanicAssignments)
                .HasForeignKey(ma => ma.AssignedJobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MechanicAssignment>()
                .HasOne(ma => ma.Mechanic)
                .WithMany(m => m.MechanicAssignments)
                .HasForeignKey(ma => ma.MechanicId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WorkWeekAssignment>()
                .HasOne(wa => wa.AssignedJob)
                .WithMany(a => a.WorkWeekAssignments)
                .HasForeignKey(wa => wa.AssignedJobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkWeekAssignment>()
                .HasOne(wa => wa.WorkWeek)
                .WithMany(w => w.WorkWeekAssignments)
                .HasForeignKey(wa => wa.WorkWeekId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Jobs>().HasData(
                new Jobs { JobId = 1, SkillLevel = 1, Name = "Oil Change", Description = "Change oil and oil filter", Priority = "Low", EstCompTime = "0.5" },
                new Jobs { JobId = 2, SkillLevel = 1, Name = "Breaks", Description = "Replace Break Pads", Priority = "Low", EstCompTime = "1" },
                new Jobs { JobId = 3, SkillLevel = 1, Name = "Air Filter", Description = "Replace Air Filter", Priority = "Low", EstCompTime = "0.25" },
                new Jobs { JobId = 4, SkillLevel = 1, Name = "Cabin Filter", Description = "Replace Cabin Filter", Priority = "Low", EstCompTime = "0.25" },
                new Jobs { JobId = 5, SkillLevel = 1, Name = "Tire Mounting And Balancing", Description = "Balance And Install/Mount New Tires", Priority = "Low", EstCompTime = "2" },
                new Jobs { JobId = 6, SkillLevel = 1, Name = "Windshield Wipers", Description = "Replace Windshield Wipers", Priority = "Low", EstCompTime = "0.25" },
                new Jobs { JobId = 7, SkillLevel = 1, Name = "Tire Rotation", Description = "Rotate Tires", Priority = "Low", EstCompTime = "1" },
                new Jobs { JobId = 8, SkillLevel = 1, Name = "Change Fluids", Description = "Remove/Drain And Replace Vehicle's Fluids", Priority = "Low", EstCompTime = "0.5" },

                new Jobs { JobId = 9, SkillLevel = 2, Name = "Catalitic Convertor", Description = "Replace Catalitic Convertor", Priority = "Low", EstCompTime = "0.5" },
                new Jobs { JobId = 10, SkillLevel = 2, Name = "Spark Plugs", Description = "Replace Of Spark Plugs", Priority = "Low", EstCompTime = "0.25" },
                new Jobs { JobId = 11, SkillLevel = 2, Name = "Internal Engine Work", Description = "Replace Head Gasket", Priority = "Low", EstCompTime = "1" },
                new Jobs { JobId = 12, SkillLevel = 2, Name = "Water Pump", Description = "Replace Water Pump", Priority = "Low", EstCompTime = "0.25" },
                new Jobs { JobId = 13, SkillLevel = 2, Name = "Alternator", Description = "Replace Alternator", Priority = "Low", EstCompTime = "0.25" },
                new Jobs { JobId = 14, SkillLevel = 2, Name = "Intake", Description = "Replace Intake", Priority = "Low", EstCompTime = "0.5" },
                new Jobs { JobId = 15, SkillLevel = 2, Name = "Exhaust Manifold", Description = "Replace Exhaust Manifold", Priority = "Low", EstCompTime = "1" },

                new Jobs { JobId = 16, SkillLevel = 3, Name = "Clutch", Description = "Replace Clutch", Priority = "Low", EstCompTime = "6" },
                new Jobs { JobId = 17, SkillLevel = 3, Name = "Transmission", Description = "Replace Transmission", Priority = "Low", EstCompTime = "5" },
                new Jobs { JobId = 18, SkillLevel = 3, Name = "Electrical Issues", Description = "Fix Electrical Issues", Priority = "Low", EstCompTime = "3.25" },
                new Jobs { JobId = 19, SkillLevel = 3, Name = "Engine Internals", Description = "Replace Pistons And Rings", Priority = "Low", EstCompTime = "7.75" },

                new Jobs { JobId = 20, SkillLevel = 4, Name = "Engine Replacement", Description = "Replace Engine", Priority = "Low", EstCompTime = "15" },
                new Jobs { JobId = 21, SkillLevel = 4, Name = "Fix Wiring", Description = "Fix Wiring", Priority = "Low", EstCompTime = "2.5" },
                new Jobs { JobId = 22, SkillLevel = 4, Name = "Work On A Maserati", Description = "Any Work Here", Priority = "Low", EstCompTime = "1.5" },
                new Jobs { JobId = 23, SkillLevel = 4, Name = "Work On A Alpha Romeo", Description = "Any Work Here", Priority = "Low", EstCompTime = "1.5" }
            );
        }
    }
}