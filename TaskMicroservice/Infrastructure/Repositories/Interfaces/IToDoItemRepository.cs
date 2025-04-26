using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Infrastructure.Repositories.Interfaces;

public interface IToDoItemRepository
{
    Task<ToDoItem> CreateToDoItemAsync(ToDoItem toDoItem);
    
    Task<ToDoItem> UpdateToDoItemAsync(ToDoItem toDoItem);
    
    Task DeleteToDoItemAsync(ToDoItem toDoItem);
    
    Task<List<ToDoItem>> GetToDoItemsByUserIdAsync(Guid userId);
    
    Task<ToDoItem?> GetToDoItemByIdAsync(Guid toDoItemId);
}