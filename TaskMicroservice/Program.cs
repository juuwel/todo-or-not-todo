using Microsoft.EntityFrameworkCore;
using Shared;
using ToDoBackend.Application.Services.Implementation;
using ToDoBackend.Application.Services.Interfaces;
using ToDoBackend.Infrastructure;
using ToDoBackend.Infrastructure.Repositories.Implementation;
using ToDoBackend.Infrastructure.Repositories.Interfaces;
using ToDoBackend.Presentation.Apis;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ToDoItemDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

builder.Services.AddCorsPolicy(builder.Configuration);

var app = builder.Build();

// Apply any pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ToDoItemDbContext>();
    dbContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors(CorsPolicyExtension.CorsPolicyName);

app.AddTaskApi();

app.Run();