using System.Text;
using Course_Service.Data;
using Course_Service.Implementation;
using Course_Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Consul;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext and Repositories
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register Services
builder.Services.AddScoped<ICourseService, CourseService>();

// JWT Authentication Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:5050", // Same as User-Service
            ValidAudience = "https://localhost:5050", // Same as User-Service
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDFSADFdfafeitt32t2r457f4f8ewf4waefeafjewfweAEFSDAFFEWFWAEAFaffd"))
        };
    });

// Add services to the container
builder.Services.AddControllersWithViews();

// Add Consul Configuration
var consulConfig = builder.Configuration.GetSection("ConsulConfig").Get<ConsulConfig>();
var consulClient = new ConsulClient(config => config.Address = new Uri(consulConfig.Address));
builder.Services.AddSingleton<IConsulClient, ConsulClient>(_ => consulClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();

// Consul service registration
var lifetime = app.Lifetime;
var logger = app.Logger;

var registration = new AgentServiceRegistration
{
    ID = consulConfig.ServiceId,
    Name = consulConfig.ServiceName,
    Address = consulConfig.ServiceHost,
    Port = consulConfig.ServicePort
};

consulClient.Agent.ServiceRegister(registration).Wait();
lifetime.ApplicationStopped.Register(() =>
{
    logger.LogInformation("Deregistering from Consul");
    consulClient.Agent.ServiceDeregister(consulConfig.ServiceId).Wait();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public class ConsulConfig
{
    public string Address { get; set; }
    public string ServiceId { get; set; }
    public string ServiceName { get; set; }
    public string ServiceHost { get; set; }
    public int ServicePort { get; set; }
}
