using ToDoBackend.Domain.DTOs;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Application.Services.Interfaces;

public interface IToDoService
{
    Task<ToDoItemDto> CreateToDoItemAsync(CreateToDoItemDto createToDoItem);
    
    Task<ToDoItemDto> UpdateToDoItemAsync(UpdateToDoItemDto updateToDoItem);
    
    Task<ToDoItemDto> UpdateToDoItemStatusAsync(Guid toDoItemId);
    
    Task DeleteToDoItemAsync(Guid toDoItemId);
    
    Task<List<ToDoItemDto>> GetToDoItemsByUserIdAsync();
    
    Task<ToDoItemDto?> GetToDoItemByIdAsync(Guid toDoItemId);
}