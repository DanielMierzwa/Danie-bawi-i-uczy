using System;
namespace Learning.App.Views;
public partial class RegisterPage : ContentPage
{
 public RegisterPage() => InitializeComponent();
    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
    private async void OnRegisterClicked(object sender, EventArgs e)
 {
  if (string.IsNullOrWhiteSpace(NameEntry.Text) || string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text) || string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text)) { await DisplayAlert("Brak danych", "Uzupełnij wszystkie pola.", "OK"); return; }
  if (PasswordEntry.Text != ConfirmPasswordEntry.Text) { await DisplayAlert("Hasła się różnią", "Upewnij się, że oba hasła są takie same.", "OK"); return; }
  await DisplayAlert("Rejestracja", "Rejestracja nie jest jeszcze połączona z systemem kont.", "OK");
 }
 private async void OnLoginClicked(object sender, EventArgs e) => await Navigation.PushAsync(new LoginPage());
}
