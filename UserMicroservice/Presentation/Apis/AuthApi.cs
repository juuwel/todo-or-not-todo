using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;
using UserMicroservice.Application.Interfaces;
using UserMicroservice.Domain.Entities;

namespace UserMicroservice.Presentation.Apis;

public static class AuthApi
{
    public static RouteGroupBuilder AddAuthApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/Auth");

        api.MapPost("/Login", Login)
            .WithName("Login")
            .AllowAnonymous();

        api.MapPost("/Register", Register)
            .WithName("Register")
            .AllowAnonymous();

        return api;
    }

    public static async Task<Results<Ok<AuthResponse>, ProblemHttpResult>> Login(
        UserManager<ApplicationUser> userManager,
        IJwtService jwtService,
        [FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Detail = "Wrong username or password",
                Status = StatusCodes.Status400BadRequest,
                Title = "WrongUserNameOrPassword"
            });
        }

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Detail = "Wrong username or password",
                Status = StatusCodes.Status400BadRequest,
                Title = "WrongUserNameOrPassword"
            });
        }

        var result = await userManager.CheckPasswordAsync(user, request.Password);
        if (!result)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Detail = "Wrong username or password",
                Status = StatusCodes.Status400BadRequest,
                Title = "WrongUserNameOrPassword"
            });
        }

        var roles = await userManager.GetRolesAsync(user);
        var token = jwtService.GenerateJwtToken(user, roles, null);

        return TypedResults.Ok(new AuthResponse()
        {
            Email = user.Email!,
            UserId = user.Id,
            Token = token
        });
    }

    public static async Task<Results<Ok, ProblemHttpResult>> Register(
        UserManager<ApplicationUser> userManager,
        [FromBody] RegisterRequest request)
    {
        throw new NotImplementedException();


        return TypedResults.Ok();
    }
}