using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learning.App.Models;
using Learning.App.Views;
using System.Collections.ObjectModel;
using Learning.App.ViewModels;
using Learning.App.Services;

namespace Learning.App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _materialsVisible = false;
        [ObservableProperty]
        private bool _utworzKontoVisible = true;
        [ObservableProperty]
        private bool _kontoVisible = false;
        [ObservableProperty]
        private string _userToken;
  
        public ObservableCollection<AbstractMaterial> Materials { get; set; } = new();

        private readonly AuthService _authService;
        public MainViewModel(AuthService authService)
        {
            _authService = authService;

            Materials.Add(new QuizMaterial(1, true, "Anatol", "Quiz 1", new List<int>() { 1, 2 }));
            Materials.Add(new QuizMaterial(2, false, "Mietek", "Quiz 2", new List<int>() { 1 }));
            Materials.Add(new FlashcardsMaterial(3, true, "Staś", "Fiszki 1", new List<int>() { 2 }));
            Materials.Add(new FlashcardsMaterial(4, false, "Jerzy", "Fiszki 2", new List<int>() { 1, 2 }));
        }
        public void LoadToken()
        {
            var token = _authService.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                // Token istnieje – możesz go użyć np. do autoryzacji zapytania HTTP
                MaterialsVisible = true;
                KontoVisible = true;
                UtworzKontoVisible = false;
            }
            else
            {
                // Brak tokenu – użytkownik prawdopodobnie nie jest zalogowany
            }
        }

        [RelayCommand]
        private async Task MyMaterials()
        {
            await Shell.Current.GoToAsync("///"+nameof(MyMaterialsPage));
        }

        [RelayCommand]
        private async Task Account()
        {
            await Shell.Current.GoToAsync("///"+nameof(AccountPage));
        }

        [RelayCommand]
        private void OnSearchPressed()
        {
            // TODO: podłączyć wyszukiwanie materiałów.
        }
        [RelayCommand]
        private async Task Register()
        {
            await Shell.Current.GoToAsync("///" + nameof(RegisterPage));
        }
        [RelayCommand]
        private async Task Loguj()
        {
            await Shell.Current.GoToAsync("///" + nameof(LoginPage));
        }

    }
}

