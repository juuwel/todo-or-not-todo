using ToDoBackend.Domain.DTOs;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Application.Services.Interfaces;

public interface IToDoService
{
    Task<ToDoItem> CreateToDoItemAsync(ToDoItemCreateDto toDoItem, Guid userId);
    
    Task<ToDoItem> UpdateToDoItemAsync(ToDoItem toDoItem);
    
    Task<ToDoItem> UpdateToDoItemStatusAsync(Guid toDoItemId);
    
    Task DeleteToDoItemAsync(Guid toDoItemId);
    
    Task<List<ToDoItem>> GetToDoItemsByUserIdAsync(Guid userId);
    
    Task<ToDoItem?> GetToDoItemByIdAsync(Guid toDoItemId);
}