namespace Learning.Data.Models;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Login { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public string Role { get; set; } = "User"; // "Admin" lub "User"

    public bool IsPremium { get; set; } = false;

    // Relacje
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public ICollection<Material> Materials { get; set; } = new List<Material>();
    public ICollection<UserStatistic> Statistics { get; set; } = new List<UserStatistic>();
    public ICollection<UserAchievement> Achievements { get; set; } = new List<UserAchievement>();
}