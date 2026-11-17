using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learning.App.Services;
using Learning.App.Views;
using Learning.Data;
using Microsoft.EntityFrameworkCore;

namespace Learning.App.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly IDbContextFactory<LearningDbContext> _dbContextFactory;

        [ObservableProperty]
        private string userName = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        public LoginPageViewModel(AuthService authService, IDbContextFactory<LearningDbContext> dbContextFactory)
        {
            _authService = authService;
            _dbContextFactory = dbContextFactory;
        }

        [RelayCommand]
        private async Task Register()
        {
            await Shell.Current.GoToAsync("///" + nameof(RegisterPage));
        }
        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Logowanie", "Wpisz nazwę użytkownika i hasło.", "OK");
                return;
            }

            await using var db = await _dbContextFactory.CreateDbContextAsync();
            var user = await db.Users.SingleOrDefaultAsync(item => item.Login == UserName.Trim());
            if (user is null || !_authService.VerifyPassword(Password, user.PasswordHash))
            {
                await Shell.Current.DisplayAlert("Logowanie", "Nieprawidłowa nazwa użytkownika lub hasło.", "OK");
                return;
            }

            var token = "twoj_kod_jwt";
            await SecureStorage.Default.SetAsync("oauth_token", token);
            _authService.SetToken(token);
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }
        [RelayCommand]
        private async Task Home()
        {
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }
    }
}
