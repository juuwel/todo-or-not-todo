namespace ToDoBackend.Domain.DTOs;

public class ToDoItemDto
{
    public Guid Id { get; set; }
    
    public required Guid UserId { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
    public DateTime? CompletedAt { get; set; }
    
    public DateTime CreatedAt { get; set; }
}