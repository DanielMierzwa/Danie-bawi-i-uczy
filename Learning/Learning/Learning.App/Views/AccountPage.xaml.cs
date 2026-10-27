using System;

namespace Learning.App.Views;

public partial class AccountPage : ContentPage
{
    private bool _isEditing;

    public AccountPage()
    {
        InitializeComponent();
        SetEditing(false);
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (!_isEditing)
        {
            SetEditing(true);
            await DisplayAlert("Edycja profilu", "Możesz teraz edytować pola. Kliknij „Zapisz”, aby zakończyć.", "OK");
            // Tekst przycisku zmienia się na „Zapisz”.
        }
        else
        {
            SetEditing(false);
            await DisplayAlert("Profil", "Zmiany są widoczne w formularzu. Zapis do bazy danych nie jest jeszcze podłączony.", "OK");
        }
    }

    private async void OnLoginClicked(object sender, EventArgs e) => await Navigation.PushAsync(new LoginPage());
    private async void OnRegisterClicked(object sender, EventArgs e) => await Navigation.PushAsync(new RegisterPage());

    private void SetEditing(bool enabled)
    {
        _isEditing = enabled;
        NameEntry.IsReadOnly = !enabled;
        EmailEntry.IsReadOnly = !enabled;
        PasswordEntry.IsReadOnly = !enabled;
        // Przycisk znajduje się w drzewie wizualnym; wyszukujemy go po tekście.
        if (this.FindByName<Button>("EditButton") is Button editButton)
            editButton.Text = enabled ? "Zapisz" : "Edytuj";
    }
}
