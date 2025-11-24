using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            // CRITICAL: Call the base method first to set up Identity tables (AspNetUsers, etc.)
            base.OnModelCreating(builder);

            // --- Configure the Core Application Relationships ---

            // 1. Link AssignedJobs to the static Job Templates (1 AssignedJob has 1 Job Template)
            builder.Entity<AssignedJobs>()
                .HasOne(aj => aj.JobTemplate)
                .WithMany()
                .HasForeignKey(aj => aj.JobTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Customers>()
                .HasMany(c => c.AssignedJobs)
                .WithOne(aj => aj.Customer)
                .HasForeignKey(aj => aj.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Mechanics>()
                .HasMany(m => m.AssignedJobs)
                .WithOne(aj => aj.AssignedMechanic)
                .HasForeignKey(aj => aj.MechanicId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}