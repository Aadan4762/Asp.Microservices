using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Ocelot configuration setup
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

// JWT Authentication setup
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:5050", // User-Service issuer
            ValidAudience = "https://localhost:5071", // Student-Microservice audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDFSADFdfafeitt32t2r457f4f8ewf4waefeafjewfweAEFSDAFFEWFWAEAFaffd")) // Secret from User-Service
        };
    });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();  // Enable authentication
app.UseAuthorization();   // Enable authorization

await app.UseOcelot();

app.Run();
