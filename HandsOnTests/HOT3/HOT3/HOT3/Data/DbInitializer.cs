using HOT3.Models;

namespace HOT3.Data
{
    public static class DbInitializer
    {
        public static void Seed(ShopDbContext db)
        {
            if (db.Products.Any()) return;

            var products = new List<Product>
            {
                new Product {
                    Name = "Luxe Denim Jacket",
                    Slug = "luxe-denim-jacket",
                    ImageFileName = "luxe-denim-jacket.png",
                    Category = "Outerwear",
                    Manufacturer = "Luxe Apparel Co",
                    Stock = 25,
                    Price = 89.99m,
                    Description = "A stylish, premium denim jacket with a modern fit and soft interior lining."
                },
                new Product {
                    Name = "Breeze Linen Shirt",
                    Slug = "breeze-linen-shirt",
                    ImageFileName = "breeze-linen-shirt.png",
                    Category = "Shirts",
                    Manufacturer = "Breeze Wear",
                    Stock = 40,
                    Price = 44.99m,
                    Description = "Lightweight, breathable linen shirt perfect for warm weather and casual style."
                },
                new Product {
                    Name = "Urban Trainer Sneakers",
                    Slug = "urban-trainer-sneakers",
                    ImageFileName = "urban-trainer-sneakers.png",
                    Category = "Shoes",
                    Manufacturer = "Urban Fit",
                    Stock = 55,
                    Price = 69.99m,
                    Description = "Comfort-driven sneakers with a sleek design and durable sole for all-day wear."
                },
                new Product {
                    Name = "Comfy Knit Beanie",
                    Slug = "comfy-knit-beanie",
                    ImageFileName = "comfy-knit-beanie.png",
                    Category = "Accessories",
                    Manufacturer = "Cozy Threads",
                    Stock = 75,
                    Price = 19.99m,
                    Description = "Warm, soft knit beanie designed for everyday comfort and style."
                },
                new Product {
                    Name = "Voyage Cargo Pants",
                    Slug = "voyage-cargo-pants",
                    ImageFileName = "voyage-cargo-pants.png",
                    Category = "Pants",
                    Manufacturer = "Voyage Outfitters",
                    Stock = 35,
                    Price = 59.99m,
                    Description = "Durable cargo pants with multiple pockets and adjustable waist for all-day wear."
                }
            };

            db.Products.AddRange(products);
            db.SaveChanges();
        }
    }
}