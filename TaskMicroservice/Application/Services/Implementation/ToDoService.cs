using System.Security.Claims;
using Shared.Exceptions;
using ToDoBackend.Application.Services.Interfaces;
using ToDoBackend.Domain.DTOs;
using ToDoBackend.Domain.Entities;
using ToDoBackend.Infrastructure.Repositories.Interfaces;

namespace ToDoBackend.Application.Services.Implementation;

public class ToDoService(IToDoItemRepository toDoItemRepository, IHttpContextAccessor httpContextAccessor) : IToDoService
{
    public async Task<ToDoItemDto> CreateToDoItemAsync(ToDoItemCreateDto toDoItemCreateDto)
    {
        var userId = UnpackUserId();
        
        var toDoItem = new ToDoItem
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = toDoItemCreateDto.Title,
            Description = toDoItemCreateDto.Description,
            CreatedAt = DateTime.UtcNow
        };
        
        var result = await toDoItemRepository.CreateToDoItemAsync(toDoItem);
        return result.ToDto();
    }

    public async Task<ToDoItemDto> UpdateToDoItemAsync(ToDoItemUpdateDto toDoItem)
    {
        var existingToDoItem = await CheckForNullAndUserAuthorization(toDoItem.Id);
        existingToDoItem.Title = toDoItem.Title;
        existingToDoItem.Description = toDoItem.Description;
        var result = await toDoItemRepository.UpdateToDoItemAsync(existingToDoItem);
        return result.ToDto();
    }

    public async Task<ToDoItemDto> UpdateToDoItemStatusAsync(Guid toDoItemId)
    {
        var existingToDoItem = await CheckForNullAndUserAuthorization(toDoItemId);
        if (existingToDoItem.CompletedAt == null)
        {
            existingToDoItem.CompletedAt = DateTime.UtcNow;
        }
        else
        {
            existingToDoItem.CompletedAt = null;
        }
        
        var result = await toDoItemRepository.UpdateToDoItemAsync(existingToDoItem);
        return result.ToDto();
    }

    public async Task DeleteToDoItemAsync(Guid toDoItemId)
    {
        var existingToDoItem = await CheckForNullAndUserAuthorization(toDoItemId);
        await toDoItemRepository.DeleteToDoItemAsync(existingToDoItem);
    }

    public async Task<List<ToDoItemDto>> GetToDoItemsByUserIdAsync()
    {
        var userId = UnpackUserId();
        var toDoItems = await toDoItemRepository.GetToDoItemsByUserIdAsync(userId);
        return toDoItems.Select(x => x.ToDto()).ToList();
    }

    public async Task<ToDoItemDto?> GetToDoItemByIdAsync(Guid toDoItemId)
    {
        var toDoItem = await CheckForNullAndUserAuthorization(toDoItemId);
        return toDoItem.ToDto();
    }
    
    private Guid UnpackUserId()
    {
        var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new ActionUnauthorizedException("User ID claim not found.");
        }

        if (Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }

        throw new ActionUnauthorizedException("User ID claim is not a valid GUID.");
    }
    
    private async Task<ToDoItem> CheckForNullAndUserAuthorization(Guid toDoItemId)
    {
        var userId = UnpackUserId();
        var existingToDoItem = await toDoItemRepository.GetToDoItemByIdAsync(toDoItemId);
        if (existingToDoItem == null)
        {
            throw new NotFoundException("ToDo item not found");
        }

        if (existingToDoItem.UserId != userId)
        {
            throw new ActionUnauthorizedException("You are not authorized to perform this action.");
        }
        
        return existingToDoItem;
    }
}