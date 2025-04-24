using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure.Repositories.Interfaces;

public interface IToDoItemRepository
{
    Task<ToDoItem> CreateToDoItemAsync(ToDoItem toDoItem);
    
    Task<ToDoItem?> UpdateToDoItemAsync(ToDoItem toDoItem);
    
    Task<ToDoItem?> ToggleToDoItemStatusAsync(Guid toDoItemId);
    
    Task DeleteToDoItemAsync(Guid toDoItemId);
    
    Task<List<ToDoItem>> GetToDoItemsByUserIdAsync(Guid userId);
    
    Task<ToDoItem?> GetToDoItemByIdAsync(Guid toDoItemId);
}