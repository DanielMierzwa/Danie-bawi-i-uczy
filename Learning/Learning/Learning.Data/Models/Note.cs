namespace Learning.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Note
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    public string Content { get; set; } // Format .md (Markdown)

    public string? AudioFilePath { get; set; } // Ścieżka do pliku audio/nagrania

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Relacje
    public int UserId { get; set; }
    public User User { get; set; }

    public int? SubjectId { get; set; }
    public Subject? Subject { get; set; }
}