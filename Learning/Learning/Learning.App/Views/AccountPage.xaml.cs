using Learning.App.ViewModels;

namespace Learning.App.Views;

public partial class AccountPage : ContentPage
{
    public AccountPage(AccountPageViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
       
    }
}
