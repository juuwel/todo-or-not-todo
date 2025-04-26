using Microsoft.EntityFrameworkCore;
using ToDoBackend.Domain.Entities;
using ToDoBackend.Infrastructure.Repositories.Interfaces;

namespace ToDoBackend.Infrastructure.Repositories.Implementation;

public class ToDoItemRepository(ToDoItemDbContext context) : IToDoItemRepository
{
    public async Task<ToDoItem> CreateToDoItemAsync(ToDoItem toDoItem)
    {
        var result = await context.ToDoItems.AddAsync(toDoItem);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<ToDoItem> UpdateToDoItemAsync(ToDoItem toDoItem)
    {
        var result = context.ToDoItems.Update(toDoItem);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task DeleteToDoItemAsync(ToDoItem toDoItem)
    {
        context.ToDoItems.Remove(toDoItem);
        await context.SaveChangesAsync();
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