using Microsoft.EntityFrameworkCore;
using ToDoBackend.Domain.Entities;
using ToDoBackend.Infrastructure.Repositories.Interfaces;

namespace ToDoBackend.Infrastructure.Repositories.Implementation;

public class ToDoItemRepository(ToDoItemDbContext context) : IToDoItemRepository
{
    public async Task CreateToDoItemAsync(ToDoItem toDoItem)
    {
        var result = await context.ToDoItems.AddAsync(toDoItem);
        await context.SaveChangesAsync();
    }

    public async Task UpdateToDoItemAsync(ToDoItem toDoItem)
    {
        var existingToDoItem = await context.ToDoItems.FindAsync(toDoItem.Id);
        if (existingToDoItem != null)
        {
            existingToDoItem.Title = toDoItem.Title;
            existingToDoItem.Description = toDoItem.Description;
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateToDoItemStatusAsync(Guid toDoItemId)
    {
        var existingToDoItem = await context.ToDoItems.FindAsync(toDoItemId);
        if (existingToDoItem != null)
        {
            existingToDoItem.CompletedAt = DateTime.Now;
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteToDoItemAsync(Guid toDoItemId)
    {
        var existingToDoItem = await context.ToDoItems.FindAsync(toDoItemId);
        if (existingToDoItem != null)
        {
            context.ToDoItems.Remove(existingToDoItem);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<ToDoItem>> GetToDoItemsByUserIdAsync(Guid userId)
    {
        return await context.ToDoItems
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<ToDoItem?> GetToDoItemByIdAsync(Guid toDoItemId)
    {
        return await context.ToDoItems
            .FirstOrDefaultAsync(t => t.Id == toDoItemId);
    }
}