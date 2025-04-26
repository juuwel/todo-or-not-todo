using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoBackend.Application.Services.Interfaces;
using ToDoBackend.Domain.DTOs;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Presentation.Apis;

public static class ToDoApi
{
    public static RouteGroupBuilder AddTaskApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/v1/tasks");

        // Get all tasks for a user
        api.MapGet("/{userId}", GetTasksByUserId)
            .WithName("GetTasksByUserId");

        // Get a specific task by ID
        api.MapGet("/item/{taskId}", GetTaskById)
            .WithName("GetTaskById");
            
        // Create a new task
        api.MapPost("/", CreateTask)
            .WithName("CreateTask");
            
        // Update a task
        api.MapPut("/", UpdateTask)
            .WithName("UpdateTask");
            
        // Update task status
        api.MapPatch("/{taskId}/status", UpdateTaskStatus)
            .WithName("UpdateTaskStatus");
            
        // Delete a task
        api.MapDelete("/{taskId}", DeleteTask)
            .WithName("DeleteTask");

        return api;
    }
    
    private static async Task<Results<Ok<List<ToDoItem>>, ProblemHttpResult>> GetTasksByUserId(
        Guid userId, 
        [FromServices] IToDoService toDoService)
    {
        try
        {
            var tasks = await toDoService.GetToDoItemsByUserIdAsync(userId);
            return TypedResults.Ok(tasks);
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<Results<Ok<ToDoItem>, NotFound, ProblemHttpResult>> GetTaskById(
        Guid taskId, 
        [FromServices] IToDoService toDoService)
    {
        try
        {
            var task = await toDoService.GetToDoItemByIdAsync(taskId);
            return TypedResults.Ok(task);
        }
        catch (Exception ex) when (ex.Message == "ToDo item not found")
        {
            return TypedResults.NotFound();
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<Results<Created<ToDoItem>, Conflict, ProblemHttpResult>> CreateTask(
        [FromBody] CreateToDoItemDto createToDoItem, 
        [FromServices] IToDoService toDoService)
    {
        try
        {
            var newToDoItem = await toDoService.CreateToDoItemAsync(createToDoItem);
            return TypedResults.Created($"/api/v1/tasks/item/{newToDoItem.Id}", newToDoItem);
        }
        catch (Exception ex) when (ex.Message == "ToDo item already exists")
        {
            return TypedResults.Conflict();
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<Results<NoContent, NotFound, ProblemHttpResult>> UpdateTask(
        [FromBody] UpdateToDoItemDto toDoItemDto,
        [FromServices] IToDoService toDoService)
    {
        try
        {
            var exists = await toDoService.GetToDoItemByIdAsync(toDoItemDto.Id);
            if (exists == null)
            {
                return TypedResults.NotFound();
            }
        
            await toDoService.UpdateToDoItemAsync(toDoItemDto);
            return TypedResults.NoContent();
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<Results<NoContent, NotFound, ProblemHttpResult>> UpdateTaskStatus(
        Guid taskId,
        [FromServices] IToDoService toDoService)
    {
        try
        {
            var toDoItem = await toDoService.GetToDoItemByIdAsync(taskId);
            if (toDoItem == null)
            {
                return TypedResults.NotFound();
            }
            
            await toDoService.UpdateToDoItemStatusAsync(toDoItem.Id);
            return TypedResults.NoContent();
        }
        catch (Exception ex) when (ex.Message == "ToDo item not found")
        {
            return TypedResults.NotFound();
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
    
    private static async Task<Results<NoContent, NotFound, ProblemHttpResult>> DeleteTask(
        Guid taskId, 
        [FromServices] IToDoService toDoService)
    {
        try
        {
            await toDoService.DeleteToDoItemAsync(taskId);
            return TypedResults.NoContent();
        }
        catch (Exception ex) when (ex.Message == "ToDo item not found")
        {
            return TypedResults.NotFound();
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}