using Learning.App.ViewModels;

namespace Learning.App.Views;

public partial class QuizEditorPage : ContentPage
{
	public QuizEditorPage(QuizEditorViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}
