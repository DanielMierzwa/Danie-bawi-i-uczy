using Learning.App.ViewModels;

namespace Learning.App.Views;

public partial class MaterialSavePage : ContentPage
{
    public MaterialSavePage(MaterialSaveViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}
