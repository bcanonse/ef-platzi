namespace EfPlatzi.Context;

public class TaskContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }

    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Category> categoriesInit = new()
        {
            new Category
            {
                Id = Guid.Parse("dbd467ca-5628-40ec-99a1-8bd95fc78c1e"),
                Name = "Actividades pendientes",
                Description = "Actividades pendientes",
                Weight = 20
            },
            new Category
            {
                Id = Guid.Parse("4d762b80-b91b-4234-b600-ff67d9a9cc04"),
                Name = "Actividades personales",
                Description = "Actividades personales",
                Weight = 50
            },
        };

        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("categories");
            category.HasKey(field => field.Id).HasName("pk_category_id");
            category.Property(field => field.Id).HasColumnName("id");
            category.Property(field => field.Name).IsRequired().HasMaxLength(150).HasColumnName("name");
            category.Property(field => field.Description).IsRequired(false).HasColumnName("description");
            category.Property(field => field.Weight).HasColumnName("weight");

            category.HasData(categoriesInit);
        });

        List<TaskModel> tasksInit = new()
        {
            new TaskModel
            {
                Id = Guid.Parse("dbd467ca-5628-40ec-99a1-8bd95fc78c10"),
                CategoryId = Guid.Parse("dbd467ca-5628-40ec-99a1-8bd95fc78c1e"),
                Priority = Priority.Medium,
                Title = "Pago de servicios publicos",
                Description = "Pago de servicios publicos",
                CreatedDate = DateTime.Now
            },
            new TaskModel
            {
                Id = Guid.Parse("dbd467ca-5628-40ec-99a1-8bd95fc78c11"),
                CategoryId = Guid.Parse("4d762b80-b91b-4234-b600-ff67d9a9cc04"),
                Priority = Priority.High,
                Title = "Terminar el proyecto de api",
                CreatedDate = DateTime.Now
            }
        };

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
            task.Property(field => field.Description).IsRequired(false).HasColumnName("description");
            task.Property(field => field.Priority).HasColumnName("priority").IsRequired();
            task.Property(field => field.CreatedDate).HasColumnName("created_date");
            task.Ignore(field => field.Summary);

            task.HasData(tasksInit);

        });
    }
}