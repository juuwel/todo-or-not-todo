namespace Shared;

public static class CorsPolicyExtension
{
    public const string CorsPolicyName = "CorsPolicy";
   
    private const string DefaultAllowedOrigins = "http://localhost:4200";
    
    public static void AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                Console.WriteLine(configuration["AllowedOrigins"]);
                builder
                    .WithOrigins(configuration["AllowedOrigins"]?.Split(",")  ?? [DefaultAllowedOrigins])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
    }
}