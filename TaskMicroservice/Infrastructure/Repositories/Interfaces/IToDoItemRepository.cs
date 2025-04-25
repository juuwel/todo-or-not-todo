using ToDoBackend.Domain.DTOs;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure.Repositories.Interfaces;

public interface IToDoItemRepository
{
    Task CreateToDoItemAsync(ToDoItem toDoItem);
    
    Task UpdateToDoItemAsync(UpdateToDoItemDto toDoItem);
    
    Task ToggleToDoItemStatusAsync(Guid toDoItemId);
    
    Task DeleteToDoItemAsync(Guid toDoItemId);
    
    Task<List<ToDoItem>> GetToDoItemsByUserIdAsync(Guid userId);
    
    Task<ToDoItem?> GetToDoItemByIdAsync(Guid toDoItemId);
}