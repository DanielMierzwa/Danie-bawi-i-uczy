using System;
namespace Learning.App.Views;
public partial class LoginPage : ContentPage
{
 public LoginPage() => InitializeComponent();
    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }
    private async void OnLoginClicked(object sender, EventArgs e)
 {
  if (string.IsNullOrWhiteSpace(EmailEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text)) { await DisplayAlert("Brak danych", "Wpisz e-mail i hasło.", "OK"); return; }
  await DisplayAlert("Logowanie", "Logowanie nie jest jeszcze połączone z systemem kont.", "OK");
 }
 private async void OnRegisterClicked(object sender, EventArgs e) => await Navigation.PushAsync(new RegisterPage());
}
