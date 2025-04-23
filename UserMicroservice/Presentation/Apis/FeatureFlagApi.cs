using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Unleash;

namespace UserMicroservice.Presentation.Apis;

public static class FeatureFlagApi
{
    public static RouteGroupBuilder AddFeatureFlagApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("/FeatureFlag");

        api.MapGet("/IsEnabled/{featureName}", IsEnabled)
            .WithName("IsEnabled")
            .AllowAnonymous();

        return api;
    }

    private static async Task<Results<Ok<bool>, ProblemHttpResult>> IsEnabled(
        IUnleash unleash,
        [FromRoute] string featureName)
    {
        var isEnabled = unleash.IsEnabled(featureName);
        return TypedResults.Ok(isEnabled);
    }
}