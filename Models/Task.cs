namespace EfPlatzi.Models;

[Table("tasks")]
public class Task
{
    [Key]
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

    [NotMapped]
    public string Summary { get; set; }
}

public enum Priority
{
    Low,
    Medium,
    High
}