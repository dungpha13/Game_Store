namespace GameStore.Api.Routes;

public static class UsersRoute
{
    public static RouteGroupBuilder MapUsersRoute(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/users").WithParameterValidation();

        group.MapGet("/", () => Results.Ok("Get route!"));

        group.MapGet("/{id}", (string id) =>
        {
            return Results.Ok("Get route!" + id);
        });

        group.MapPut("/{id}", (string id) =>
        {
            return Results.Ok("Put route!" + id);
        });

        group.MapDelete("/{id}", (string id) =>
        {
            return Results.Ok("Delete route!" + id);
        });

        return group;
    }
}