using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        // Zapisywanie tokenu sesji
        string? oauthToken;


        [RelayCommand]
        private async Task Register()
        {
            await Shell.Current.GoToAsync("///" + nameof(RegisterPage));
        }
        [RelayCommand]
        private async Task Login()

        {
            await SecureStorage.Default.SetAsync("oauth_token", "twoj_kod_jwt");

            // Odczytywanie tokenu sesji
            oauthToken = await SecureStorage.Default.GetAsync("oauth_token");

            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }
        [RelayCommand]
        private async Task Home()
        {
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }
    }
}
