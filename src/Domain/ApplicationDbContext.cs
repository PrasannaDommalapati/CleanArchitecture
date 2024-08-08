using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain;
public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SeedRoles(modelBuilder);
    }

    private void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>()
            .HasData(new IdentityRole
            {
                Name = "SuperAdmin",
                ConcurrencyStamp = "1",
                NormalizedName = "SuperAdmin"
            },
            new IdentityRole
            {
                Name = "LocationAdmin",
                ConcurrencyStamp = "2",
                NormalizedName = "LocationAdmin"
            },
            new IdentityRole
            {
                Name = "User",
                ConcurrencyStamp = "3",
                NormalizedName = "User"
            });
    }
}
