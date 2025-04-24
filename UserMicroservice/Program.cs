using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Unleash;
using Unleash.ClientFactory;
using UserMicroservice.Application.Handlers;
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

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtConfig = builder.Configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtConfig.GetValue<string>("Key")
                                         ?? throw new NullReferenceException("JWT key cannot be null"));
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig.GetValue<string>("Issuer"),
            ValidAudience = jwtConfig.GetValue<string>("Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddProblemDetails();

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

app.UseAuthentication();
app.UseAuthorization();

// Register APIs
app.AddAuthApi();
app.AddUserApi();
app.AddFeatureFlagApi();

app.Run();
