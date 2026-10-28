using Learning.Data.Models;
using System.ComponentModel.DataAnnotations;
namespace Learning.Data.Models;

public class UserAchievement
{
    [Key]
    public int Id { get; set; }

    public DateTime UnlockedAt { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public User User { get; set; }

    public int AchievementId { get; set; }
    public Achievement Achievement { get; set; }
}