using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    // Your dbsets here
    public DbSet<Fruit> Fruits { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Your configurations here
        builder.Entity<Fruit>()
            .HasData(
                new Fruit { Id = 1, Name = "Apple", Color = "Red", Taste = Taste.Sweet },
                new Fruit { Id = 2, Name = "Banana", Color = "Yellow", Taste = Taste.Sweet },
                new Fruit { Id = 3, Name = "Lemon", Color = "Yellow", Taste = Taste.Sour },
                new Fruit { Id = 4, Name = "Lime", Color = "Green", Taste = Taste.Sour },
                new Fruit { Id = 5, Name = "Orange", Color = "Orange", Taste = Taste.Sweet },
                new Fruit { Id = 6, Name = "Pineapple", Color = "Yellow", Taste = Taste.Sweet },
                new Fruit { Id = 7, Name = "Strawberry", Color = "Red", Taste = Taste.Sweet }
            );
        base.OnModelCreating(builder);
    }
}