using ToDoBackend.Application.Services.Interfaces;
using ToDoBackend.Domain.DTOs;
using ToDoBackend.Domain.Entities;
using ToDoBackend.Infrastructure.Repositories.Interfaces;

namespace ToDoBackend.Application.Services.Implementation;

public class ToDoService(IToDoItemRepository toDoItemRepository) : IToDoService
{
    public async Task<ToDoItem> CreateToDoItemAsync(ToDoItemCreateDto toDoItemCreateDto, Guid userId)
    {
        var toDoItem = new ToDoItem
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = toDoItemCreateDto.Title,
            Description = toDoItemCreateDto.Description,
            CreatedAt = DateTime.UtcNow
        };
        
        return await toDoItemRepository.CreateToDoItemAsync(toDoItem);
    }

    public async Task<ToDoItem> UpdateToDoItemAsync(ToDoItem toDoItem)
    {
        var existingToDoItem = await toDoItemRepository.GetToDoItemByIdAsync(toDoItem.Id);
        if (existingToDoItem == null)
        {
            throw new Exception("ToDo item not found");
        }
        
        return await toDoItemRepository.UpdateToDoItemAsync(toDoItem);
    }

    public async Task<ToDoItem> UpdateToDoItemStatusAsync(Guid toDoItemId)
    {
        var existingToDoItem = await toDoItemRepository.GetToDoItemByIdAsync(toDoItemId);
        if (existingToDoItem == null)
        {
            throw new Exception("ToDo item not found");
        }
        return await toDoItemRepository.ToggleToDoItemStatusAsync(toDoItemId);
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