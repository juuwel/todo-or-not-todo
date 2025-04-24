using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.Handlers;
using Unleash;
using Unleash.ClientFactory;
using UserMicroservice.Application.Services;
using UserMicroservice.Application.Services.Interfaces;
using UserMicroservice.Core.Configuration;
using UserMicroservice.Domain.Entities;
using UserMicroservice.Infrastructure;
using UserMicroservice.Presentation.Apis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Register Options
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCustomAuthenticationAndAuthorization(builder.Configuration);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IUnleash>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var settings = new UnleashSettings
    {
        AppName = "UserMicroservice",
        UnleashApi = new Uri(configuration["Unleash:ApiUrl"]!),
        CustomHttpHeaders = new Dictionary<string, string>
        {
            { "Authorization", configuration["Unleash:ApiToken"]! }
        },
    };
    return new UnleashClientFactory().CreateClient(settings, synchronousInitialization: true);
});

builder.Services.AddCorsPolicy(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

// Apply any pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseStatusCodePages();

app.UseCors(CorsPolicyExtension.CorsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

// Register APIs
app.AddAuthApi();
app.AddUserApi();
app.AddFeatureFlagApi();

app.Run();
