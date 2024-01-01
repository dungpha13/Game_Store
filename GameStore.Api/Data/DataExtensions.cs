using GameStore.Api.Repositories;
using GameStore.Api.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void InitializeDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
        dbContext.Database.Migrate();
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connString = configuration.GetConnectionString("GameStoreContext");

        services.AddSqlServer<DataBaseContext>(connString)
                    .AddScoped<IGamesRepository, GameRepository>()
                    .AddScoped<IUsersRepository, UsersRepository>()
                    .AddScoped<ICartsRepository, CartsRepository>();

        return services;
    }
}