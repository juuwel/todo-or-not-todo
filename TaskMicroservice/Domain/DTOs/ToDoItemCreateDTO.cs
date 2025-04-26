using System.ComponentModel.DataAnnotations;

namespace ToDoBackend.Domain.DTOs;

public class ToDoItemCreateDto
{
    [MaxLength(64)]
    public required string Title { get; set; }
    
    [MaxLength(128)]
    public required string Description { get; set; }
}