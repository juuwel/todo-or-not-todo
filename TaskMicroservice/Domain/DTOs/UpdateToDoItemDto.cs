namespace ToDoBackend.Domain.DTOs;

public class UpdateToDoItemDto
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    
    public required string Description { get; set; }
}