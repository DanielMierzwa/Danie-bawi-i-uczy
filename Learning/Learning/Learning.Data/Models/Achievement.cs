namespace Learning.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Achievement
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
}