using System;

namespace Learning.App.Views;

public partial class MyMaterialsPage : ContentPage
{
    public MyMaterialsPage()
    {
        InitializeComponent();
    }

    private async void OnHomeClicked(object sender, EventArgs e) =>
        await Navigation.PopToRootAsync();
private async void OnAccountClicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new AccountPage());

    private void OnSearchPressed(object sender, EventArgs e)
    {
        // TODO: podłączyć wyszukiwanie materiałów użytkownika.
    }

    private async void OnAddClicked(object sender, EventArgs e) =>
        await DisplayAlert("Dodaj materiał", "Wybór sposobu dodawania materiału zostanie dodany później.", "OK");
}
