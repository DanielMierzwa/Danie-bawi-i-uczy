using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using Learning.App.Models;

namespace Learning.App.ViewModels
{
    /// <summary>
    /// Ujednolicony viewmodel dla zapisu zarówno QuizMaterial jak i FlashcardsMaterial.
    /// Odbiera dane przez QueryProperty z nawigacji.
    /// </summary>
    [QueryProperty(nameof(MaterialType), "materialType")]
    [QueryProperty(nameof(Title), "quizTitle")]
    [QueryProperty(nameof(SelectedCategory), "selectedCategory")]
    [QueryProperty(nameof(Description), "description")]
    [QueryProperty(nameof(QuestionsJson), "questions")]
    [QueryProperty(nameof(FlashcardsJson), "flashcards")]
    public partial class MaterialSaveViewModel : ObservableObject
    {
        private readonly MainViewModel main;

        // Przechowywane obiekty po zapisaniu
        private AbstractMaterial? savedMaterial = null;
        private List<Question>? quizQuestions = null;
        private List<Flashcard>? flashcardsList = null;

        public MaterialSaveViewModel(MainViewModel main)
        {
            this.main = main;
        }

        // ----- Observable Properties -----
        [ObservableProperty]
        private string materialType = "quiz";

        [ObservableProperty]
        private string title = "";

        [ObservableProperty]
        private string? selectedCategory;

        [ObservableProperty]
        private bool isPublic = false;

        [ObservableProperty]
        private string description = "";

        [ObservableProperty]
        private string questionsJson = "";
        partial void OnQuestionsJsonChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    quizQuestions = JsonSerializer.Deserialize<List<Question>>(value);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error deserializing questions: {ex.Message}");
                    quizQuestions = null;
                }
            }
        }

        [ObservableProperty]
        private string flashcardsJson = "";

        partial void OnFlashcardsJsonChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    flashcardsList = JsonSerializer.Deserialize<List<Flashcard>>(value);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error deserializing flashcards: {ex.Message}");
                    flashcardsList = null;
                }
            }
        }

        public List<string> Categories { get; } = new()
        {
            "Matematyka", "Fizyka", "Chemia", "Język polski", "Język angielski", "Historia", "Informatyka"
        };

        // ----- Komendy -----
        [RelayCommand]
        private async Task Save()
        {
            string? error = Validate();
            if (error != null)
            {
                await Shell.Current.DisplayAlert("Nie można zapisać", error, "OK");
                return;
            }

            try
            {
                var categories = new List<int>();
                if (!string.IsNullOrEmpty(selectedCategory))
                {
                    int index = Categories.IndexOf(selectedCategory);
                    if (index >= 0)
                        categories.Add(index + 1);
                }

                // Tworzenie odpowiedniego obiektu na podstawie typu materiału
                if (materialType.ToLower() == "quiz")
                {
                    var quiz = new QuizMaterial(main.Materials.Count + 1, isPublic, "Użytkownik", title, categories);
                    // Dodaj pytania jeśli są dostępne
                    if (quizQuestions != null)
                    {
                        foreach (var q in quizQuestions)
                            quiz.AddQuestion(q);
                    }
                    savedMaterial = quiz;
                }
                else if (materialType.ToLower() == "flashcard")
                {
                    var flashcards = new FlashcardsMaterial(main.Materials.Count + 1, isPublic, "Użytkownik", title, categories);
                    // Dodaj fiszki jeśli są dostępne
                    if (flashcardsList != null)
                    {
                        foreach (var f in flashcardsList)
                            flashcards.AddFlashcard(f);
                    }
                    savedMaterial = flashcards;
                }

                // Dodaj do kolekcji materiałów
                if (savedMaterial != null)
                {
                    main.Materials.Add(savedMaterial);
                }

                Reset();
                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Błąd", $"Nie udało się zapisać materiału: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("..");
        }

        // ----- Walidacja -----
        private string? Validate()
        {
            if (string.IsNullOrWhiteSpace(title))
                return "Podaj tytuł materiału.";

            if (string.IsNullOrEmpty(selectedCategory) && Categories.Count > 0)
                return "Wybierz kategorię.";

            return null;
        }

        // ----- Reset -----
        private void Reset()
        {
            Title = "";
            SelectedCategory = null;
            IsPublic = false;
            Description = "";
            savedMaterial = null;
            quizQuestions = null;
            flashcardsList = null;
            QuestionsJson = "";
            FlashcardsJson = "";
        }

        // ----- Pobranie zapisanego materiału -----
        public AbstractMaterial? GetSavedMaterial()
        {
            return savedMaterial;
        }
    }
}
