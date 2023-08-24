namespace EfPlatzi.Context;

public class TaskContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }

    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("categories");
            category.HasKey(field => field.Id).HasName("pk_category_id");
            category.Property(field => field.Id).HasColumnName("id");
            category.Property(field => field.Name).IsRequired().HasMaxLength(150).HasColumnName("name");
            category.Property(field => field.Description).HasColumnName("description");
            category.Property(field => field.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<TaskModel>(task =>
        {
            task.ToTable("tasks");
            task.HasKey(field => field.Id).HasName("pk_task_id");

            task.HasOne(field => field.Category)
                .WithMany(category => category.Tasks)
                .HasForeignKey(field => field.CategoryId)
                .HasConstraintName("fk_tasks_category_id");

            task.Property(field => field.Id).HasColumnName("id");
            task.Property(field => field.CategoryId).HasColumnName("category_id");
            task.Property(field => field.Title).IsRequired().HasMaxLength(200).HasColumnName("title");
            task.Property(field => field.Description).HasColumnName("description");
            task.Property(field => field.Priority).HasColumnName("priority").IsRequired();
            task.Property(field => field.CreatedDate).HasColumnName("created_date");
            task.Ignore(field => field.Summary);

        });
    }
}