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

    if (id.HasValue)
        return Results.Ok(tasks.Where(task => (int)task.Priority == id).ToList());

    return Results.Ok(tasks);
});

app.MapGet("/api/tasks/{id:guid}", async ([FromServices] TaskContext taskContext, [FromRoute] Guid id) =>
{
    var tasks = await taskContext.Tasks.Include(task => task.Category)
        .Where(task => task.Id == id).FirstOrDefaultAsync();

    return Results.Ok(tasks);
}).WithName("GetTaskById")
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

app.MapPost("/api/tasks/", async (
    [FromServices] TaskContext taskContext,
    [FromBody] TaskModel task) =>
{

    if (await taskContext.Categories.FindAsync(task.CategoryId) is null)
        return Results.BadRequest(new { message = "Category not found" });

    task.Id = Guid.NewGuid();
    task.CreatedDate = DateTime.Now;

    await taskContext.AddAsync(task);
    await taskContext.SaveChangesAsync();

    return Results.CreatedAtRoute(
        routeName: "GetTaskById",
        routeValues: new { id = task.Id }, 
        value: task
    );
}).WithName("CreateTask")
    .Produces(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest);

app.Run();
