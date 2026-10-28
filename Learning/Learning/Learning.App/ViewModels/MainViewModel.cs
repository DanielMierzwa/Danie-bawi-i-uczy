using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learning.App.Models;
using Learning.App.Views;
using System.Collections.ObjectModel;
using Learning.App.ViewModels;

namespace Learning.App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _MaterialsVisible = false;
        [ObservableProperty]
        private bool _UtworzKontoVisible = true;
        [ObservableProperty]
        private bool _KontoVisible = false;
        [ObservableProperty]
        private string _UserToken;

        public void ProfileViewModel()
        {
            // Wywołujemy asynchroniczną metodę ładowania tokenu
            LoadTokenAsync();
        }

        private async void LoadTokenAsync()
        {
            // Pobieramy bezpiecznie token zapisany wcześniej w MainViewModel
            _UserToken = await SecureStorage.Default.GetAsync("oauth_token");
        }
        partial void OnUserTokenChanging(string value)
        { ///Do dokonczenia
            if (value== null) 
            {
                _MaterialsVisible = true;
                _KontoVisible =true;
                _UtworzKontoVisible = false;
            }


        }


        public ObservableCollection<AbstractMaterial> Materials { get; set; } = new();

        public MainViewModel()
        {
           

            Materials.Add(new QuizMaterial(1, true, "Anatol", "Quiz 1", new List<int>() { 1, 2 }));
            Materials.Add(new QuizMaterial(2, false, "Mietek", "Quiz 2", new List<int>() { 1 }));
            Materials.Add(new FlashcardsMaterial(3, true, "Staś", "Fiszki 1", new List<int>() { 2 }));
            Materials.Add(new FlashcardsMaterial(4, false, "Jerzy", "Fiszki 2", new List<int>() { 1, 2 }));
        
        
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

