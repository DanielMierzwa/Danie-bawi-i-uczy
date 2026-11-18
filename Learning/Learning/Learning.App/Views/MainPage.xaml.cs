using Learning.App.ViewModels;

namespace Learning.App.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _vm;
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.LoadToken();
    }
}

