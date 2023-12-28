using GameStore.Api.Data;
using GameStore.Api.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

app.Services.InitializeDb();

app.MapGamesRoutes();

app.Run();
