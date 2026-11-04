using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learning.App.Views;
using System.Windows.Input;

namespace Learning.App.ViewModels
{
    public partial class AccountPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private string nameEntry = string.Empty;

        [ObservableProperty]
        private string emailEntry = string.Empty;

        [ObservableProperty]
        private string passwordEntry = string.Empty;

        [ObservableProperty]
        private string editButtonText = "Edytuj";

        public bool IsEntryReadOnly => !IsEditing;

        public AccountPageViewModel()
        {
            IsEditing = false;
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("///"+nameof(MainPage));
        }

        [RelayCommand]
        private async Task OnEdit()
        {
            if (!IsEditing)
            {
                IsEditing = true;
                EditButtonText = "Zapisz";
                await Application.Current.MainPage.DisplayAlert("Edycja profilu", "Możesz teraz edytować pola. Kliknij Zapisz, aby zakończyć.", "OK");
            }
            else
            {
                IsEditing = false;
                EditButtonText = "Edytuj";
                await Application.Current.MainPage.DisplayAlert("Profil", "Zmiany są widoczne w formularzu. Zapis do bazy danych nie jest jeszcze podłączony.", "OK");
            }
        }


    }
}
