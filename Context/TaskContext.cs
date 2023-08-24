namespace EfPlatzi.Context;

public class TaskContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }

    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }


}