using Learning.App.ViewModels;

namespace Learning.App.Views;

public partial class QuizSavePage : ContentPage
{
	public QuizSavePage(QuizEditorViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}
