namespace Learning.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Question
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string QuestionText { get; set; } // Treść pytania lub przód fiszki

    [Required]
    public string QuestionType { get; set; } = "Closed"; // "Closed", "Open", "Flashcard"

    public string? CorrectAnswerText { get; set; } // Dla pytań otwartych lub tyłu fiszki

    // Relacje
    public int MaterialId { get; set; }
    public Material Material { get; set; }

    public ICollection<AnswerOption> Options { get; set; } = new List<AnswerOption>();
}