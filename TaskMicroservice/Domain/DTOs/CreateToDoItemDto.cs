namespace ToDoBackend.Domain.DTOs;
// DTO for creating a ToDoItem
public class CreateToDoItemDto
{
    public Guid Id { get; set; }
    
    public required Guid UserId { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
}