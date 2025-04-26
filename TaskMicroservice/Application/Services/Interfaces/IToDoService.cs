using ToDoBackend.Domain.DTOs;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Application.Services.Interfaces;

public interface IToDoService
{
    Task<ToDoItem> CreateToDoItemAsync(ToDoItemCreateDto toDoItem);
    
    Task<ToDoItem> UpdateToDoItemAsync(ToDoItemUpdateDto toDoItem);
    
    Task<ToDoItem> UpdateToDoItemStatusAsync(Guid toDoItemId);
    
    Task DeleteToDoItemAsync(Guid toDoItemId);
    
    Task<List<ToDoItem>> GetToDoItemsByUserIdAsync();
    
    Task<ToDoItem?> GetToDoItemByIdAsync(Guid toDoItemId);
}