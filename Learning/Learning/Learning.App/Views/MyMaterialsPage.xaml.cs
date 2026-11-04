using Learning.App.ViewModels;

namespace Learning.App.Views;

public partial class MyMaterialsPage : ContentPage
{
    public MyMaterialsPage(MyMaterialsPageViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();

    }
}