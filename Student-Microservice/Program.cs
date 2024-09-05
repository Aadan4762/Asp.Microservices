using Microsoft.EntityFrameworkCore;
using Student_Microservice.Data;
using Student_Microservice.Implementation;
using Student_Microservice.Interface;
using Student_Microservice.Services;

var builder = WebApplication.CreateBuilder(args);
// Register DbContext and Repositories
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 39)) // Replace with your MySQL version
    )
);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
// Register Services

builder.Services.AddScoped<IStudentService, StudentService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();