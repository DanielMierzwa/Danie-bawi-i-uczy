namespace Learning.Data.Models;

using System.ComponentModel.DataAnnotations;

public class AnswerOption
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string OptionText { get; set; } // Treść wariantu odpowiedzi

    public bool IsCorrect { get; set; } = false; // Czy odpowiedź jest poprawna (checkbox)

    // Relacja
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}