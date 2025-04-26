using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;
using ToDoBackend.Application.Services.Interfaces;
using ToDoBackend.Domain.DTOs;
using ToDoBackend.Domain.Entities;

namespace ToDoBackend.Presentation.Apis;

public static class ToDoApi
{
    public static RouteGroupBuilder AddTaskApi(this IEndpointRouteBuilder app)
    {
        var api = app
            .MapGroup("/api/v1/tasks")
            .RequireAuthorization();

        // Get all tasks for a user
        api.MapGet("/", GetTasksByUserId)
            .WithName("GetTasksByUserId");

        // Get a specific task by ID
        api.MapGet("/item/{taskId:guid}", GetTaskById)
            .WithName("GetTaskById");

        // Create a new task
        api.MapPost("/", CreateTask)
            .WithName("CreateTask");

        // Update a task
        api.MapPut("/", UpdateTask)
            .WithName("UpdateTask");

        // Update task status
        api.MapPatch("/status/{taskId:guid}", UpdateTaskStatus)
            .WithName("UpdateTaskStatus");

        // Delete a task
        api.MapDelete("/{taskId:guid}", DeleteTask)
            .WithName("DeleteTask");

        return api;
    }

    private static async Task<Results<Ok<List<ToDoItem>>, ProblemHttpResult>> GetTasksByUserId(
        [FromServices] IToDoService toDoService,
        HttpContext context)
    {
        var userId = UnpackUserId(context);
        var tasks = await toDoService.GetToDoItemsByUserIdAsync(userId);
        return TypedResults.Ok(tasks);
    }

    private static async Task<Results<Ok<ToDoItem>, NotFound, ProblemHttpResult>> GetTaskById(
        Guid taskId,
        [FromServices] IToDoService toDoService)
    {
        var task = await toDoService.GetToDoItemByIdAsync(taskId);
        return TypedResults.Ok(task);
    }

    private static async Task<Results<Created<ToDoItem>, Conflict, ProblemHttpResult>> CreateTask(
        [FromBody] ToDoItemCreateDto toDoItemCreateDto,
        [FromServices] IToDoService toDoService,
        HttpContext context
    )
    {
        var userId = UnpackUserId(context);
        var item = await toDoService.CreateToDoItemAsync(toDoItemCreateDto, userId);
        return TypedResults.Created($"/api/v1/tasks/item/{item.Id}", item);
    }

    private static async Task<Results<NoContent, NotFound, ProblemHttpResult>> UpdateTask(
        [FromBody] ToDoItem toDoItem,
        [FromServices] IToDoService toDoService)
    {
        await toDoService.UpdateToDoItemAsync(toDoItem);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound, ProblemHttpResult>> UpdateTaskStatus(
        Guid taskId,
        [FromServices] IToDoService toDoService)
    {
        await toDoService.UpdateToDoItemStatusAsync(taskId);
        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound, ProblemHttpResult>> DeleteTask(
        Guid taskId,
        [FromServices] IToDoService toDoService)
    {
        await toDoService.DeleteToDoItemAsync(taskId);
        return TypedResults.NoContent();
    }

    private static Guid UnpackUserId(HttpContext context)
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
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
}