using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User_Service.Models.Entities;

namespace User_Service.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<ApplicationUser> Register { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = StaticUserRoles.USER, NormalizedName = StaticUserRoles.USER.ToUpper() },
            new IdentityRole { Name = StaticUserRoles.ADMIN, NormalizedName = StaticUserRoles.ADMIN.ToUpper() },
            new IdentityRole { Name = StaticUserRoles.OWNER, NormalizedName = StaticUserRoles.OWNER.ToUpper() }
        );
    }
}
