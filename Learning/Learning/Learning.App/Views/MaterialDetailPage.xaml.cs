using System;

namespace Learning.App.Views;

public partial class MaterialDetailPage : ContentPage
{
    public MaterialDetailPage()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
    }

    private async void OnQuizClicked(object sender, EventArgs e) =>
        await DisplayAlert("Quiz", "Widok quizu zostanie zaimplementowany później.", "OK");

    private async void OnFlashcardsClicked(object sender, EventArgs e) =>
        await DisplayAlert("Fiszki", "Widok fiszek zostanie zaimplementowany później.", "OK");

    private async void OnNoteClicked(object sender, EventArgs e) =>
        await DisplayAlert("Notatka", "Edytor notatek zostanie zaimplementowany później.", "OK");
}
