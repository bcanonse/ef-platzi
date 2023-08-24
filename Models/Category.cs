namespace EfPlatzi.Models;

public class Category
{

    public Guid Id { get; set; }

    public string Name { get; set; }
    public string? Description { get; set; }

    public int Weight { get; set; }

    [JsonIgnore]
    public virtual ICollection<TaskModel> Tasks { get; set; }
}
