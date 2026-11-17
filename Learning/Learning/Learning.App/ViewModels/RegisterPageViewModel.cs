using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learning.App.Services;
using Learning.App.Views;
using Learning.Data;
using Learning.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Learning.App.ViewModels
{
    public partial class RegisterPageViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly IDbContextFactory<LearningDbContext> _dbContextFactory;

        [ObservableProperty]
        private string userName = string.Empty;

        [ObservableProperty]
        private string userPassword = string.Empty;

        [ObservableProperty]
        private string confirmUserPassword = string.Empty;

        public RegisterPageViewModel(AuthService authService, IDbContextFactory<LearningDbContext> dbContextFactory)
        {
            _authService = authService;
            _dbContextFactory = dbContextFactory;
        }

        [RelayCommand]
        private async Task Register()
        {
            var normalizedLogin = UserName.Trim();
            if (string.IsNullOrWhiteSpace(normalizedLogin) || string.IsNullOrWhiteSpace(UserPassword))
            {
                await Shell.Current.DisplayAlert("Rejestracja", "Wpisz nazwę użytkownika i hasło.", "OK");
                return;
            }

            if (UserPassword.Length < 6)
            {
                await Shell.Current.DisplayAlert("Rejestracja", "Hasło musi mieć co najmniej 6 znaków.", "OK");
                return;
            }

            if (UserPassword != ConfirmUserPassword)
            {
                await Shell.Current.DisplayAlert("Rejestracja", "Hasła muszą być takie same.", "OK");
                return;
            }

            await using var db = await _dbContextFactory.CreateDbContextAsync();
            if (await db.Users.AnyAsync(user => user.Login == normalizedLogin))
            {
                await Shell.Current.DisplayAlert("Rejestracja", "Taka nazwa użytkownika już istnieje.", "OK");
                return;
            }

            db.Users.Add(new User
            {
                Login = normalizedLogin,
                PasswordHash = _authService.HashPassword(UserPassword)
            });
            await db.SaveChangesAsync();

            await Shell.Current.DisplayAlert("Rejestracja", "Konto zostało utworzone.", "OK");
            await Shell.Current.GoToAsync("///" + nameof(LoginPage));
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }

        [RelayCommand]
        private async Task Login()
        {
            await Shell.Current.GoToAsync("///" + nameof(LoginPage));
        }
    }
}
