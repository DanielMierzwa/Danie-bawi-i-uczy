using System;
using Learning.App.ViewModels;
namespace Learning.App.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }

    private async void OnMyMaterialsClicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new MyMaterialsPage());
private async void OnAccountClicked(object sender, EventArgs e) =>
        await Navigation.PushAsync(new AccountPage());

    private void OnSearchPressed(object sender, EventArgs e)
    {
        // TODO: podłączyć wyszukiwanie materiałów.
    }
}
