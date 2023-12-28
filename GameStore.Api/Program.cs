using GameStore.Api.Data;
using GameStore.Api.Repositories;
using GameStore.Api.Routes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGamesRepository, InMemGamesRepository>();

var connString = builder.Configuration.GetConnectionString("GameStoreContex");
builder.Services.AddSqlServer<DataBaseContext>(connString);

var app = builder.Build();

app.MapGamesRoutes();

app.Run();
