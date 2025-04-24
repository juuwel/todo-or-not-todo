using Microsoft.EntityFrameworkCore;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure;

public class ToDoItemDbContext(DbContextOptions<ToDoItemDbContext> options) : DbContext(options)
{
    public virtual DbSet<ToDoItem> ToDoItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ToDoItem>(entity =>
        {
            // Set CompletedAt to null by default when a new item is created
            entity.Property(e => e.CompletedAt)
                .IsRequired(false)
                .HasDefaultValue(null);
        });
    }
}