using Microsoft.EntityFrameworkCore;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure;

public class ToDoItemDbContext : DbContext
{
    // TODO: Configure entity relationships
    public virtual DbSet<ToDoItem> ToDoItems { get; set; }
    
    public ToDoItemDbContext(DbContextOptions<ToDoItemDbContext> options) : base(options)
    {
    }
    
}