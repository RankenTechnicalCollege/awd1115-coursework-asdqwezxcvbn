using Auctions.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auctions.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Listing> Listings { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Prevent multiple cascade delete paths
            builder.Entity<Bid>()
                .HasOne(b => b.Listing)
                .WithMany(l => l.Bids)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(c => c.Listing)
                .WithMany(l => l.Comments)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Listing>()
                .HasOne(l => l.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Bid>()
                .HasOne(b => b.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}