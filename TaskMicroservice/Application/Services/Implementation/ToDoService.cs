using ToDoBackend.Application.Services.Interfaces;
using ToDoBackend.Domain.Entities;
using ToDoBackend.Infrastructure.Repositories.Interfaces;

namespace ToDoBackend.Application.Services.Implementation;

public class ToDoService(IToDoItemRepository toDoItemRepository) : IToDoService
{
    public async Task CreateToDoItemAsync(ToDoItem toDoItem)
    {
        var existingToDoItem = await toDoItemRepository.GetToDoItemByIdAsync(toDoItem.Id);
        if (existingToDoItem != null)
        {
            throw new Exception("ToDo item already exists");
        }
        await toDoItemRepository.CreateToDoItemAsync(toDoItem);
        
    }

    public async Task UpdateToDoItemAsync(ToDoItem toDoItem)
    {
        var existingToDoItem = await toDoItemRepository.GetToDoItemByIdAsync(toDoItem.Id);
        if (existingToDoItem == null)
        {
            throw new Exception("ToDo item not found");
        }
        await toDoItemRepository.UpdateToDoItemAsync(toDoItem);
    }

    public async Task UpdateToDoItemStatusAsync(Guid toDoItemId)
    {
        var existingToDoItem = await toDoItemRepository.GetToDoItemByIdAsync(toDoItemId);
        if (existingToDoItem == null)
        {
            throw new Exception("ToDo item not found");
        }
        await toDoItemRepository.UpdateToDoItemStatusAsync(toDoItemId);
    }

    public async Task DeleteToDoItemAsync(Guid toDoItemId)
    {
        var existingToDoItem = await toDoItemRepository.GetToDoItemByIdAsync(toDoItemId);
        if (existingToDoItem == null)
        {
            throw new Exception("ToDo item not found");
        }
        await toDoItemRepository.DeleteToDoItemAsync(toDoItemId);
    }

    public async Task<List<ToDoItem>> GetToDoItemsByUserIdAsync(Guid userId)
    {
        var toDoItems = await toDoItemRepository.GetToDoItemsByUserIdAsync(userId);
        return toDoItems.ToList();
    }

    public async Task<ToDoItem?> GetToDoItemByIdAsync(Guid toDoItemId)
    {
        var toDoItem = await toDoItemRepository.GetToDoItemByIdAsync(toDoItemId);
        if (toDoItem == null)
        {
            throw new Exception("ToDo item not found");
        }
        return toDoItem;
    }
}