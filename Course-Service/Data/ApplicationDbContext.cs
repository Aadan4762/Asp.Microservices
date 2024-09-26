using Course_Service.Entity;
using Microsoft.EntityFrameworkCore;

namespace Course_Service.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
}