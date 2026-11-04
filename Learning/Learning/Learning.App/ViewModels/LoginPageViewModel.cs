using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learning.App.Services;
using Learning.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.App.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        public LoginPageViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task Register()
        {
            await Shell.Current.GoToAsync("///" + nameof(RegisterPage));
        }
        [RelayCommand]
        private async Task Login()
        {
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
