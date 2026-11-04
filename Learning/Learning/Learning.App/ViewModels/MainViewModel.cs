using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learning.App.Models;
using Learning.App.Views;
using System.Collections.ObjectModel;

namespace Learning.App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
    

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
            Console.Write("XDD");
            await Shell.Current.GoToAsync("///"+nameof(AccountPage));
        }

        [RelayCommand]
        private void OnSearchPressed()
        {
            // TODO: podłączyć wyszukiwanie materiałów.
        }

        
    }
}

