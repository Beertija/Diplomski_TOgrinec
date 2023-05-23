using KolodvorApp.Server.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.ConfigureServices(builder.Environment, builder.Configuration);

var app = builder.Build();

app.Configure()
    .MigrateDatabase()
    .SeedData()
    .Run();