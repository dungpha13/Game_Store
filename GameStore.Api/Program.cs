using GameStore.Api.Repositories;
using GameStore.Api.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();

var app = builder.Build();

app.MapGamesRoutes();

app.Run();
