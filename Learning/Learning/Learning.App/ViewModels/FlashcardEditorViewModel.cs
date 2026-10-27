using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Learning.App.Models;
using Learning.App.ViewModels;
using Learning.App.Views;
using System.Collections.Generic;

public partial class FlashcardEditorViewModel : ObservableObject
{
    private bool _isInternalUpdate = false;

    [ObservableProperty]
    private List<Flashcard> flashcards;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentCardIndex))]
    private int currentIndex = 0;

    [ObservableProperty]
    private string frontValue = string.Empty;

    [ObservableProperty]
    private string backValue = string.Empty;

    public string CurrentCardIndex => Flashcards?.Count > 0 ? $"{CurrentIndex + 1} / {Flashcards.Count}" : "0 / 0";

    public FlashcardEditorViewModel(List<Flashcard>? existingCards = null)
    {
        Flashcards = existingCards ?? new List<Flashcard>();
        if (Flashcards.Count == 0)
        {
            Flashcards.Add(new Flashcard());
        }
        LoadCardData();
    }

    partial void OnFrontValueChanged(string value)
    {
        if (!_isInternalUpdate && Flashcards.Count > 0 && CurrentIndex >= 0 && CurrentIndex < Flashcards.Count)
        {
            Flashcards[CurrentIndex].Front = value;
        }
    }

    partial void OnBackValueChanged(string value)
    {
        if (!_isInternalUpdate && Flashcards.Count > 0 && CurrentIndex >= 0 && CurrentIndex < Flashcards.Count)
        {
            Flashcards[CurrentIndex].Back = value;
        }
    }

    private void LoadCardData()
    {
        _isInternalUpdate = true;
        if (Flashcards.Count > 0 && CurrentIndex >= 0 && CurrentIndex < Flashcards.Count)
        {
            FrontValue = Flashcards[CurrentIndex].Front ?? string.Empty;
            BackValue = Flashcards[CurrentIndex].Back ?? string.Empty;
        }
        _isInternalUpdate = false;
        OnPropertyChanged(nameof(CurrentCardIndex));
    }

    [RelayCommand]
    private void NextButton()
    {
        if (CurrentIndex < Flashcards.Count - 1)
        {
            CurrentIndex++;
            LoadCardData();
        }
    }

    [RelayCommand]
    private void BackButton()
    {
        if (CurrentIndex > 0)
        {
            CurrentIndex--;
            LoadCardData();
        }
    }

    [RelayCommand]
    private void AddCard()
    {
        Flashcards.Add(new Flashcard { Front = "", Back = "" });
        CurrentIndex = Flashcards.Count - 1;
        LoadCardData();
    }

    [RelayCommand]
    private void DeleteCard()
    {
        if (Flashcards.Count > 1)
        {
            Flashcards.RemoveAt(CurrentIndex);
            if (CurrentIndex >= Flashcards.Count)
            {
                CurrentIndex = Flashcards.Count - 1;
            }
            LoadCardData();
        }
        else
        {
            Flashcards[0].Front = string.Empty;
            Flashcards[0].Back = string.Empty;
            LoadCardData();
        }
    }

    [RelayCommand]
    private async void SaveCards()
    {
        SaveFlashcardViewModel.Flashcards = Flashcards;
        await Shell.Current.GoToAsync(nameof(SaveFlashcard));
    }

    [RelayCommand]
    private async Task ExitEditor()
    {

        bool answer = await Application.Current!.MainPage!.DisplayAlert(
            "Niezapisane zmiany",
            "Czy na pewno chcesz wyjść? Niezapisane zmiany zostaną utracone.",
            "Tak",
            "Nie");
        if (answer)
        {
         await Shell.Current.GoToAsync("..");
        }
    }
}