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
        private async void Save()
        {
            if(string.IsNullOrWhiteSpace(Title) || Category == -1){
                await Application.Current!.MainPage!.DisplayAlert("Błąd", "Wypełnij wszystkie wymagane pola!", "OK");
            }
            else
            {
                if(Title.Length > 50)
                {
                    await Application.Current!.MainPage!.DisplayAlert("Błąd", "Tytuł nie może być dłuższy niż 50 znaków!", "OK");
                    return;
                }
                else
                {
                    await Application.Current!.MainPage!.DisplayAlert("Sukces", "Fiszki zostały zapisane!", "OK");
                    await Shell.Current.GoToAsync("///MainPage");
                }
                
            }
        }

        [RelayCommand]
        private async void Cancel()
        {
            bool answer = await Application.Current!.MainPage!.DisplayAlert("Anuluj", "Czy na pewno chcesz anulować?", "Tak", "Nie");
            if (answer)
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
