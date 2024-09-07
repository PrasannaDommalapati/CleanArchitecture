using Domain.Entities;
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

    public DbSet<Applicant> Applicants { get; set; }

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
                Name = "User",
                ConcurrencyStamp = "2",
                NormalizedName = "User"
            });
    }
}
