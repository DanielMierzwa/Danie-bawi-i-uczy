using Learning.App.ViewModels;
namespace Learning.App.Views;

public partial class FlashcardEditorPage : ContentPage
{
	public FlashcardEditorPage(FlashcardEditorViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}