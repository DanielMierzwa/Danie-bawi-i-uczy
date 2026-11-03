namespace Learning.Data.Models;

using System.ComponentModel.DataAnnotations;

public class UserStatistic
{
    [Key]
    public int Id { get; set; }

    public int StudyTimeMinutes { get; set; }
    public int CompletedQuizzesCount { get; set; }
    public int RepeatedFlashcardsCount { get; set; }
    public double SuccessRate { get; set; } // Skuteczność w %

    public int UserId { get; set; }
    public User User { get; set; }
}