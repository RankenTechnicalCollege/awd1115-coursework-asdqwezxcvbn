using Microsoft.EntityFrameworkCore;

namespace CarInventorySystem
{
    public class CarDb : DbContext
    {
        public CarDb(DbContextOptions<CarDb> options) : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
    }
}
