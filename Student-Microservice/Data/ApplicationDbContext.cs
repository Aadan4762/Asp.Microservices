using Microsoft.EntityFrameworkCore;
using Student_Microservice.Models;

namespace Student_Microservice.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Student> Students { get; set; }
}