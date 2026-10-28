using Learning.App.ViewModels;
using System;
namespace Learning.App.Views;
public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}

