using Learning.App.ViewModels;

namespace Learning.App.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }

    }
}
