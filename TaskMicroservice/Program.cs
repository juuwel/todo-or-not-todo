using Microsoft.EntityFrameworkCore;
using ToDoBackend.Application.Services.Implementation;
using ToDoBackend.Application.Services.Interfaces;
using ToDoBackend.Infrastructure;
using ToDoBackend.Infrastructure.Repositories.Implementation;
using ToDoBackend.Infrastructure.Repositories.Interfaces;
using ToDoBackend.Presentation.Apis;

using ServiceDefaults;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ToDoItemDbContext>(options =>
{
    var connectionString = EnvironmentHelper.GetValue("POSTGRES_CONNECTION_STRING", builder.Configuration, "ConnectionStrings:DefaultConnection");
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();

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

app.AddTaskApi();

app.Run();