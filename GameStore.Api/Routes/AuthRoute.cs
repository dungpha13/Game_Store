namespace GameStore.Api.Routes;

public static class AuthRoute
{
    public static RouteGroupBuilder MapAuthRoute(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api").WithParameterValidation();

        group.MapPost("/register", () =>
        {
            return Results.Ok("Register route!");
        });

        group.MapPost("/login", () =>
        {

            return Results.Ok("Login route!");
        });

        return group;
    }

}