using Learning.App.ViewModels;
namespace Learning.App.Views;

public partial class SaveFlashcard : ContentPage
{
	public SaveFlashcard(SaveFlashcardViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}