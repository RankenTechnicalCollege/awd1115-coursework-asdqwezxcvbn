using Microsoft.EntityFrameworkCore;

namespace ADW1115HOT2.Models
{
    public class SalesOrdersContext : DbContext
    {
        public SalesOrdersContext(DbContextOptions<SalesOrdersContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithOne(c => c.Product)
                .HasForeignKey<Product>(p => p.CategoryID);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Wheels" },
                new Category { CategoryID = 2, CategoryName = "Frames" },
                new Category { CategoryID = 3, CategoryName = "Handlebars" },
                new Category { CategoryID = 4, CategoryName = "Saddles" },
                new Category { CategoryID = 5, CategoryName = "Brakes" },
                new Category { CategoryID = 6, CategoryName = "Drivetrains" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    ProductName = "AeroFlo ATB Wheels",
                    ProductDescShort = "Durable mountain bike wheels",
                    ProductDescLong = "The AeroFlo ATB wheels are designed for rugged trails with lightweight aluminum construction.",
                    ProductImage = "aeroflo-atb.jpg",
                    ProductPrice = 250.00m,
                    ProductQty = 10,
                    CategoryID = 1
                },
                new Product
                {
                    ProductID = 2,
                    ProductName = "TrailBlazer Frame",
                    ProductDescShort = "Lightweight alloy frame",
                    ProductDescLong = "TrailBlazer provides strength and comfort for cross-country adventures.",
                    ProductImage = "trailblazer-frame.jpg",
                    ProductPrice = 500.00m,
                    ProductQty = 5,
                    CategoryID = 2
                },
                new Product
                {
                    ProductID = 3,
                    ProductName = "ErgoPro Handlebars",
                    ProductDescShort = "Ergonomic racing handlebars",
                    ProductDescLong = "Designed for maximum grip and aerodynamic performance on road bikes.",
                    ProductImage = "ergopro-handlebars.jpg",
                    ProductPrice = 120.00m,
                    ProductQty = 15,
                    CategoryID = 3
                },
                new Product
                {
                    ProductID = 4,
                    ProductName = "ComfortRide Saddle",
                    ProductDescShort = "High-density foam saddle",
                    ProductDescLong = "ComfortRide saddles provide lasting comfort for long-distance riders.",
                    ProductImage = "comfortride-saddle.jpg",
                    ProductPrice = 75.00m,
                    ProductQty = 20,
                    CategoryID = 4
                },
                new Product
                {
                    ProductID = 5,
                    ProductName = "ProStop Brakes",
                    ProductDescShort = "High-performance brake system",
                    ProductDescLong = "Reliable stopping power for road and mountain bikes with easy adjustment features.",
                    ProductImage = "prostop-brakes.jpg",
                    ProductPrice = 90.00m,
                    ProductQty = 12,
                    CategoryID = 5
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}