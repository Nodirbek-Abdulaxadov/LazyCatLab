using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    // Your dbsets here

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Your configurations here

        base.OnModelCreating(builder);
    }
}