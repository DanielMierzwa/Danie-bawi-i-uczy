using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learning.App.Views;

namespace Learning.App.ViewModels
{
    public partial class MyMaterialsPageViewModel : ObservableObject
    {
        public MyMaterialsPageViewModel()
        {
        }

        [RelayCommand]
        private async Task OnHome()
        {
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }

        [RelayCommand]
        private async Task Account()
        {
            await Shell.Current.GoToAsync("///"+nameof(AccountPage));
        }

        [RelayCommand]
        private async Task OnSearch()
        {

        }
        [RelayCommand]
        private async Task Editflashcard() 
        {
            await Shell.Current.GoToAsync("///" + nameof(FlashcardEditorPage));
        }
        [RelayCommand]
        private async Task Editquiz()
        {
            await Shell.Current.GoToAsync("///" + nameof(QuizEditorPage));
        }


    }
}
