namespace ToDoBackend.Domain.DTOs;

public class ToDoItemCreateDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
}