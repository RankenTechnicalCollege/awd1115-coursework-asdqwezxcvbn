using CH08Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace CH08Lab.Data
{
    public class TripDbContext : DbContext
    {
        public TripDbContext(DbContextOptions<TripDbContext> options) : base(options){}
        public DbSet<Trip> Trips { get; set; } = null!;
        public DbSet<Destination> Destinations { get; set; } = null!;
        public DbSet<Accommodation> Accommodations { get; set; } = null!;
        public DbSet<Activity> Activities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Destination)
                .WithMany(d => d.Trips)
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Accommodation)
                .WithMany(a => a.Trips)
                .HasForeignKey(t => t.AccommodationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                .HasMany(t => t.Activities)
                .WithMany(a => a.Trips);
        }
    }
}