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

    private static async Task<Results<Ok<List<ToDoItemDto>>, ProblemHttpResult>> GetTasksByUserId(
        [FromServices] IToDoService toDoService)
    {
        var tasks = await toDoService.GetToDoItemsByUserIdAsync();
        return TypedResults.Ok(tasks);
    }

    private static async Task<Results<Ok<ToDoItemDto>, NotFound, ProblemHttpResult>> GetTaskById(
        [FromRoute] Guid taskId,
        [FromServices] IToDoService toDoService)
    {
        var task = await toDoService.GetToDoItemByIdAsync(taskId);
        return TypedResults.Ok(task);
    }

    private static async Task<Results<Created<ToDoItemDto>, Conflict, ProblemHttpResult>> CreateTask(
        [FromBody] ToDoItemCreateDto toDoItemCreateDto,
        [FromServices] IToDoService toDoService
    )
    {
        var item = await toDoService.CreateToDoItemAsync(toDoItemCreateDto);
        return TypedResults.Created($"/api/v1/tasks/item/{item.Id}", item);
    }

    private static async Task<Results<Ok<ToDoItemDto>, NotFound, ProblemHttpResult>> UpdateTask(
        [FromBody] ToDoItemUpdateDto toDoItem,
        [FromServices] IToDoService toDoService)
    {
        var item = await toDoService.UpdateToDoItemAsync(toDoItem);
        return TypedResults.Ok(item);
    }

    private static async Task<Results<Ok<ToDoItemDto>, NotFound, ProblemHttpResult>> UpdateTaskStatus(
        Guid taskId,
        [FromServices] IToDoService toDoService)
    {
        var item = await toDoService.UpdateToDoItemStatusAsync(taskId);
        return TypedResults.Ok(item);
    }

    private static async Task<Results<NoContent, NotFound, ProblemHttpResult>> DeleteTask(
        Guid taskId,
        [FromServices] IToDoService toDoService)
    {
        await toDoService.DeleteToDoItemAsync(taskId);
        return TypedResults.NoContent();
    }
}