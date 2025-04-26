using System.ComponentModel.DataAnnotations;
using ToDoBackend.Domain.DTOs;

namespace ToDoBackend.Domain.Entities;

public class ToDoItem
{
    public Guid Id { get; set; }
    
    public required Guid UserId { get; set; }
    
    [MaxLength(64)]
    public required string Title { get; set; }
    
    [MaxLength(128)]
    public required string Description { get; set; }
    
    public DateTime? CompletedAt { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public ToDoItemDto ToDto()
    {
        return new ToDoItemDto
        {
            Id = Id,
            UserId = UserId,
            Title = Title,
            Description = Description,
            CompletedAt = CompletedAt,
            CreatedAt = CreatedAt
        };
    }
}