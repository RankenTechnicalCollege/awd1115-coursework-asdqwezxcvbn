using CH06Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace CH06Lab.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Topic> Topics { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Faq> Faqs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Topic>().HasData(
                new Topic { Id = "js", Name = "JavaScript" },
                new Topic { Id = "csharp", Name = "C#" },
                new Topic { Id = "html", Name = "HTML" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = "setup", Name = "Setup" },
                new Category { Id = "usage", Name = "Usage" },
                new Category { Id = "hist", Name = "History" }
            );

            modelBuilder.Entity<Faq>().HasData(
                new Faq
                {
                    FaqId = 1,
                    Question = "How do I include a script tag?",
                    Answer = "Use <script src=\"...\"></script> before the closing body tag.",
                    TopicId = "js",
                    CategoryId = "setup"
                },
                new Faq
                {
                    FaqId = 2,
                    Question = "What is var vs let?",
                    Answer = "let and const have block scope, var is function scoped.",
                    TopicId = "js",
                    CategoryId = "usage"
                },
                new Faq
                {
                    FaqId = 3,
                    Question = "How do I create a class in C#?",
                    Answer = "Use the class keyword and place members inside braces.",
                    TopicId = "csharp",
                    CategoryId = "usage"
                },
                new Faq
                {
                    FaqId = 4,
                    Question = "When was HTML created?",
                    Answer = "HTML was created in 1991 by Tim Berners-Lee.",
                    TopicId = "html",
                    CategoryId = "hist"
                }
            );
        }
    }
}
