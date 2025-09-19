using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CH04Lab.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Family" },
                new Category { CategoryId = 2, Name = "Friends" },
                new Category { CategoryId = 3, Name = "Work" },
                new Category { CategoryId = 4, Name = "Other" }
            );

            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    FirstName = "Alex",
                    LastName = "Morgan",
                    Phone = "555-123-4567",
                    Email = "alex.morgan@example.com",
                    CategoryId = 1,
                    DateAdded = new DateTime(2024, 1, 1)
                },
                new Contact
                {
                    ContactId = 2,
                    FirstName = "Delores",
                    LastName = "Del Rio",
                    Phone = "555-987-6543",
                    Email = "delores@hotmail.com",
                    CategoryId = 2,
                    DateAdded = new DateTime(2022, 9, 27)
                },
                new Contact
                {
                    ContactId = 3,
                    FirstName = "Efran",
                    LastName = "Hererra",
                    Phone = "555-456-7890",
                    Email = "efren@aol.com",
                    CategoryId = 3,
                    DateAdded = new DateTime(1998, 12, 25)
                }
            );
        }
    }
}
