using Learning.App.Views;

namespace Learning.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(QuizSavePage), typeof(QuizSavePage));
            Routing.RegisterRoute(nameof(FlashcardEditorPage), typeof(FlashcardEditorPage));
            Routing.RegisterRoute(nameof(SaveFlashcard), typeof(SaveFlashcard));
        }
    }
}
