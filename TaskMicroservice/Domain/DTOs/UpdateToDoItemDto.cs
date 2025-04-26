using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Domain.DTOs;

public class UpdateToDoItemDto
{
    public required Guid Id { get; set; }
    
    [MaxLength(64)]
    public required string Title { get; set; }
    
    [MaxLength(128)]
    public required string Description { get; set; }
}