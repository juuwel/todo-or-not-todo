namespace ToDoBackend.Presentation.Apis;

public static class ToDoApi
{
    public static RouteGroupBuilder AddTaskApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/api/v1/tasks");

        /*api.MapGet("/{subscriptionType}", GetSubscribers)
            .WithName("Get all subscribers for a subscription type");*/

        return api;
    }
    
    /*private static async Task<Results<Ok<GetSubscriptionDto>, ProblemHttpResult>> CreateSubscription(
        [FromBody] CreateSubscriptionDto request, [FromServices] ISubscriberService subscriberService,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await subscriberService.SubscribeAsync(request);
            var dto = new GetSubscriptionDto
            {
                Id = result.Id,
                Email = result.Subscriber!.Email,
                SubscriptionType = result.SubscriptionType!.Type
            };
            return TypedResults.Ok(dto);
        }
        catch (NotFoundException e)
        {
            Log.Error("Subscription type not found: {SubscriptionType}", request.SubscriptionType);
            return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (BadRequestException e)
        {
            Log.Error("Bad request: {Error}", e.Message);
            return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
        } 
        catch (Exception e)
        {
            Log.Error("An error occurred while creating subscription: {Error}", e.Message);
            return TypedResults.Problem(e.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
    }*/
}