using BirdStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BirdStore.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bird> Birds { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Category entity
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "PERRUCHE", DisplayOrder = 1 },
                new Category { Id = 2, Name = "INSÉPARABLE", DisplayOrder = 2 },
                new Category { Id = 3, Name = "CANARI", DisplayOrder = 3 }
            );

            // Seed data for Bird entity
            modelBuilder.Entity<Bird>().HasData(
                new Bird
                {
                    Id = 1,
                    Name = "CANARI MIX COULEURS",
                    Color = "red",
                    Description = "Les canaris domestiques existent dans une variété de mutations génétiques, produisant des couleurs telles que le jaune, le rouge, le vert, le blanc, le brun et d'autres nuances.",
                    Price = 90,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Bird
                {
                    Id = 2,
                    Name = "PERRUCHE ONDULÉErn",
                    Color = "red",
                    Description = "Les canaris domestiques existent dans une variété de mutations génétiques, produisant des couleurs telles que le jaune, le rouge, le vert, le blanc, le brun et d'autres nuances.",
                    Price = 190,
                    CategoryId = 2,
                    ImageUrl = ""
                }
            );
        }
    }
}
