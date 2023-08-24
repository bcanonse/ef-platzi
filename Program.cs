var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<TaskContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconnection", async ([FromServices] TaskContext taskContext) =>
{
    await taskContext.Database.EnsureCreatedAsync();

    return Results.Ok($"Database in memory {taskContext.Database.IsSqlServer()}");
});

app.Run();
