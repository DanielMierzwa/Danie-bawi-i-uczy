using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learning.App.Models;
using Learning.App.Views;

namespace Learning.App.ViewModels
{
    public partial class SaveFlashcardViewModel : ObservableObject
    {
        public static List<Flashcard> Flashcards { get; set; } = new();
        [ObservableProperty]
        private string _title;
        [ObservableProperty]
        private int _category;
        [ObservableProperty]
        private bool _isPublic;
        [ObservableProperty]
        private string _description;





        [RelayCommand]
        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Title) || Category == -1)
            {
                await Application.Current!.MainPage!.DisplayAlert("Błąd", "Wypełnij wszystkie wymagane pola!", "OK");
                return;
            }

            if (Title.Length > 50)
            {
                await Application.Current!.MainPage!.DisplayAlert("Błąd", "Tytuł nie może być dłuższy niż 50 znaków!", "OK");
                return;
            }

            // Kieruj na MaterialSavePage dla fiszek
            var categoryName = Category >= 0 && Category < 3 ? new[] { "Opcja 1", "Opcja 2", "Opcja 3" }[Category] : "";
            var queryParameter = $"materialType=flashcard&quizTitle={Uri.EscapeDataString(Title)}&selectedCategory={Uri.EscapeDataString(categoryName)}&description={Uri.EscapeDataString(Description ?? "")}";
            await Shell.Current.GoToAsync($"MaterialSavePage?{queryParameter}");
        }

        [RelayCommand]
        private async Task Cancel()
        {
            bool answer = await Application.Current!.MainPage!.DisplayAlert("Anuluj", "Czy na pewno chcesz anulować?", "Tak", "Nie");
            if (answer)
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
