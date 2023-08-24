var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<TaskContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconnection", async ([FromServices] TaskContext taskContext) =>
{
    await taskContext.Database.EnsureCreatedAsync();

    return Results.Ok($"Database in memory {taskContext.Database.IsSqlServer()}");
});

app.MapGet("/api/tasks", async (
    [FromServices] TaskContext taskContext,
    [FromQuery(Name = "priority")] int? id) =>
{

    var tasks = await taskContext.Tasks.Include(task => task.Category)
        .ToListAsync();

    if(id is not null || id >= -1)
        return Results.Ok(tasks.Where(task => (int)task.Priority == id).ToList());

    return Results.Ok(tasks);
});

app.MapGet("/api/tasks/{id:guid}", async ([FromServices] TaskContext taskContext, [FromRoute] Guid id) =>
{
    var tasks = await taskContext.Tasks.Include(task => task.Category)
        .Where(task => task.Id == id).FirstOrDefaultAsync();

    return Results.Ok(tasks);
});

app.Run();
