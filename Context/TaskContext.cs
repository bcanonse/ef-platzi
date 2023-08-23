namespace EfPlatzi.Context;

public class TaskContext : DbContext
{
    public DbSet<Category> Categories;
    public DbSet<TaskModel> Tasks;

    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    
}