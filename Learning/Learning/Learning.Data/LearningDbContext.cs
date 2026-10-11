using Learning.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Learning.Data;

public class LearningDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<AnswerOption> AnswerOptions { get; set; }
    public DbSet<UserStatistic> UserStatistics { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<UserAchievement> UserAchievements { get; set; }

    public LearningDbContext(DbContextOptions<LearningDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "learningDb.db3");
        optionsBuilder.UseSqlite($"Data Source={path}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1:N - Użytkownik ma wiele Notatek
        modelBuilder.Entity<Note>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notes)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1:N - Przedmiot ma wiele Notatek
        modelBuilder.Entity<Note>()
            .HasOne(n => n.Subject)
            .WithMany(s => s.Notes)
            .HasForeignKey(n => n.SubjectId)
            .OnDelete(DeleteBehavior.SetNull);

        // 1:N - Użytkownik ma wiele Materiałów
        modelBuilder.Entity<Material>()
            .HasOne(m => m.User)
            .WithMany(u => u.Materials)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1:N - Przedmiot ma wiele Materiałów
        modelBuilder.Entity<Material>()
            .HasOne(m => m.Subject)
            .WithMany(s => s.Materials)
            .HasForeignKey(m => m.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1:N - Materiał ma wiele Pytań
        modelBuilder.Entity<Question>()
            .HasOne(q => q.Material)
            .WithMany(m => m.Questions)
            .HasForeignKey(q => q.MaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1:N - Pytanie ma wiele Opcji Odpowiedzi
        modelBuilder.Entity<AnswerOption>()
            .HasOne(a => a.Question)
            .WithMany(q => q.Options)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1:N - Użytkownik ma wiele Statystyk
        modelBuilder.Entity<UserStatistic>()
            .HasOne(us => us.User)
            .WithMany(u => u.Statistics)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // N:N (z tabelą łączącą UserAchievement) - Użytkownik <-> Osiągnięcia
        modelBuilder.Entity<UserAchievement>()
            .HasOne(ua => ua.User)
            .WithMany(u => u.Achievements)
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserAchievement>()
            .HasOne(ua => ua.Achievement)
            .WithMany(a => a.UserAchievements)
            .HasForeignKey(ua => ua.AchievementId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
