using Learning.App.ViewModels;
using Learning.App.Views;
using Microsoft.Extensions.Logging;

namespace Learning.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<FlashcardEditorViewModel>();
            builder.Services.AddSingleton<FlashcardEditorPage>();
            builder.Services.AddSingleton<SaveFlashcardViewModel>();
            builder.Services.AddSingleton<SaveFlashcard>();

            return builder.Build();
        }
    }
}
