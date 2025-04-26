using ToDoBackend.Application.Services.Interfaces;
using ToDoBackend.Domain.DTOs;
using ToDoBackend.Domain.Entities;
using ToDoBackend.Infrastructure.Repositories.Interfaces;

namespace ToDoBackend.Application.Services.Implementation;

public class ToDoService(IToDoItemRepository toDoItemRepository) : IToDoService
{
    public async Task<ToDoItem> CreateToDoItemAsync(CreateToDoItemDto createtoDoItem)
    {
        
        var toDoItem = new ToDoItem
        {
            Id = Guid.NewGuid(),
            UserId = createtoDoItem.UserId,
            Title = createtoDoItem.Title,
            Description = createtoDoItem.Description,
            CreatedAt = DateTime.UtcNow,
            CompletedAt = null
        };
        await toDoItemRepository.CreateToDoItemAsync(toDoItem);
        return toDoItem;

    }

    public async Task UpdateToDoItemAsync(UpdateToDoItemDto toDoItem)
    {
        var existingToDoItem = await toDoItemRepository.GetToDoItemByIdAsync(toDoItem.Id);
        if (existingToDoItem == null)
        {
            throw new Exception("ToDo item not found");
        }
        
        existingToDoItem.Title = toDoItem.Title;
        existingToDoItem.Description = toDoItem.Description;
        
        await toDoItemRepository.UpdateToDoItemAsync(existingToDoItem);
    }

    public async Task UpdateToDoItemStatusAsync(Guid toDoItemId)
    {
        var existingToDoItem = await toDoItemRepository.GetToDoItemByIdAsync(toDoItemId);
        if (existingToDoItem != null)
        {
            if (existingToDoItem.CompletedAt == null)
            {
                existingToDoItem.CompletedAt = DateTime.UtcNow;
            }
            else
            {
                existingToDoItem.CompletedAt = null;
            }
        }
        else
        {
            throw new Exception("ToDo item not found");
        }
        await toDoItemRepository.ToggleToDoItemStatusAsync(toDoItemId);
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