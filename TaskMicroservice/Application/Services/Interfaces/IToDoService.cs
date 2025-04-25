using ToDoBackend.Domain.DTOs;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Application.Services.Interfaces;

public interface IToDoService
{
    Task CreateToDoItemAsync(ToDoItem toDoItem);
    
    Task UpdateToDoItemAsync(UpdateToDoItemDto toDoItem);
    
    Task UpdateToDoItemStatusAsync(Guid toDoItemId);
    
    Task DeleteToDoItemAsync(Guid toDoItemId);
    
    Task<List<ToDoItem>> GetToDoItemsByUserIdAsync(Guid userId);
    
    Task<ToDoItem?> GetToDoItemByIdAsync(Guid toDoItemId);
}