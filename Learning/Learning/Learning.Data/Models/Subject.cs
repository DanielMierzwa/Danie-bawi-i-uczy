namespace Learning.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Subject
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } // np. Matematyka, Fizyka, Chemia

    public ICollection<Material> Materials { get; set; } = new List<Material>();
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}