using Microsoft.EntityFrameworkCore;
using TripLog.Models;

namespace TripLog.Data
{
    public class TripDbContext : DbContext
    {
        public TripDbContext(DbContextOptions<TripDbContext> options)
            : base(options) { }

        public DbSet<Trip> Trips { get; set; } = null!;
    }
}