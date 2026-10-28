namespace Learning.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Material
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    public string Type { get; set; } // "Quiz" lub "Flashcard"

    public bool IsPublic { get; set; } = false; // false = prywatne, true = publiczne

    // Relacje
    public int UserId { get; set; }
    public User User { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

    public ICollection<Question> Questions { get; set; } = new List<Question>();
}