using System;
namespace Learning.App.Views;

using ViewModels;
public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
   