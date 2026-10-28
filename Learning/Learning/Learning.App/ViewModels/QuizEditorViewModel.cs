using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Learning.App.Models;

namespace Learning.App.ViewModels
{
    // Viewmodel edytora quizu (QuizEditorPage + QuizSavePage).
    // Pytania to obiekty Question, a na końcu z nich powstaje QuizMaterial.
    public partial class QuizEditorViewModel : ObservableObject
    {
        private readonly MainViewModel main;

        private List<Question> questions = new() { NewQuestion() };
        private int currentIndex = 0;

        public QuizEditorViewModel(MainViewModel main)
        {
            this.main = main;
        }

        // ----- strona zapisu -----
        [ObservableProperty] private string quizTitle = "";
        [ObservableProperty] private string? selectedCategory;
        [ObservableProperty] private string description = ""; // QuizMaterial nie ma pola na opis, więc się nie zapisuje

        public List<string> Categories { get; } = new()
        {
            "Matematyka", "Fizyka", "Chemia", "Język polski", "Język angielski", "Historia", "Informatyka"
        };

        // ----- pytanie na ekranie -----
        [ObservableProperty] private string questionCounterText = "pytanie 1 / 1";
        [ObservableProperty] private string questionText = "";

        // 4 miejsca na odpowiedzi; pierwsze AnswerCount z nich jest używane
        public ObservableCollection<string> AnswerTexts { get; } = new() { "", "", "", "" };
        public ObservableCollection<bool> AnswerCorrect { get; } = new() { false, false, false, false };

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsAnswer2Visible), nameof(IsAnswer3Visible), nameof(IsAnswer4Visible))]
        [NotifyCanExecuteChangedFor(nameof(AddAnswerCommand), nameof(DeleteAnswerCommand))]
        private int answerCount = 2;

        public bool IsAnswer2Visible => AnswerCount >= 2;
        public bool IsAnswer3Visible => AnswerCount >= 3;
        public bool IsAnswer4Visible => AnswerCount >= 4;

        private static Question NewQuestion() => new Question("", new[] { "", "" }, -1);

        // pytanie z ekranu -> Question na liście
        private void Commit()
        {
            string[] answers = AnswerTexts.Take(AnswerCount).ToArray();
            // Question ma jedną dobrą odpowiedź - bierzemy pierwszą zaznaczoną (-1 = żadna)
            int good = AnswerCorrect.Take(AnswerCount).ToList().IndexOf(true);

            questions[currentIndex] = new Question(QuestionText, answers, good);
        }

        // Question z listy -> pytanie na ekranie
        private void Show()
        {
            var q = questions[currentIndex];
            QuestionText = q.Quest;
            for (int i = 0; i < 4; i++)
            {
                AnswerTexts[i] = i < q.Answers.Length ? q.Answers[i] : "";
                AnswerCorrect[i] = i == q.GoodAnswerIndex;
            }
            AnswerCount = q.Answers.Length;

            QuestionCounterText = $"pytanie {currentIndex + 1} / {questions.Count}";
            PreviousQuestionCommand.NotifyCanExecuteChanged();
            NextQuestionCommand.NotifyCanExecuteChanged();
            DeleteQuestionCommand.NotifyCanExecuteChanged();
        }

        // ----- pytania -----
        [RelayCommand(CanExecute = nameof(CanGoPrevious))]
        private void PreviousQuestion()
        {
            Commit();
            currentIndex--;
            Show();
        }
        private bool CanGoPrevious() => currentIndex > 0;

        [RelayCommand(CanExecute = nameof(CanGoNext))]
        private void NextQuestion()
        {
            Commit();
            currentIndex++;
            Show();
        }
        private bool CanGoNext() => currentIndex < questions.Count - 1;

        [RelayCommand]
        private void AddQuestion()
        {
            Commit();
            questions.Add(NewQuestion());
            currentIndex = questions.Count - 1;
            Show();
        }

        [RelayCommand(CanExecute = nameof(CanDeleteQuestion))]
        private async Task DeleteQuestion()
        {
            bool yes = await Shell.Current.DisplayAlert("Usuwanie", "Czy na pewno chcesz usunąć to pytanie?", "tak", "nie");
            if (!yes) return;

            questions.RemoveAt(currentIndex);
            if (currentIndex >= questions.Count) currentIndex = questions.Count - 1;
            Show();
        }
        private bool CanDeleteQuestion() => questions.Count > 1;

        // ----- odpowiedzi -----
        [RelayCommand(CanExecute = nameof(CanAddAnswer))]
        private void AddAnswer()
        {
            AnswerCount++;
        }
        private bool CanAddAnswer() => AnswerCount < 4;

        // number = numer odpowiedzi (1-4) z CommandParameter
        [RelayCommand(CanExecute = nameof(CanDeleteAnswer))]
        private void DeleteAnswer(string number)
        {
            int index = int.Parse(number) - 1;

            AnswerTexts.RemoveAt(index);
            AnswerTexts.Add("");
            AnswerCorrect.RemoveAt(index);
            AnswerCorrect.Add(false);
            AnswerCount--;
        }
        private bool CanDeleteAnswer() => AnswerCount > 1;

        // ----- przejścia między stronami -----
        [RelayCommand]
        private async Task Exit()
        {
            bool yes = await Shell.Current.DisplayAlert("Wyjście", "Czy na pewno chcesz wyjść bez zapisywania? Twoje zmiany zostaną usunięte.", "tak", "nie");
            if (!yes) return;

            Reset();
            await Shell.Current.GoToAsync("//MainPage");
        }

        [RelayCommand]
        private async Task Save()
        {
            Commit();
            await Shell.Current.GoToAsync("//QuizSavePage");
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("//QuizEditorPage");
        }

        // ----- zapis: tworzy QuizMaterial -----
        [RelayCommand]
        private async Task SaveQuiz()
        {
            string? error = Validate();
            if (error != null)
            {
                await Shell.Current.DisplayAlert("Nie można zapisać", error, "OK");
                return;
            }

            var categories = new List<int>();
            if (SelectedCategory != null)
                categories.Add(Categories.IndexOf(SelectedCategory) + 1);

            // id, autor i widoczność na razie tymczasowe
            var quiz = new QuizMaterial(main.Materials.Count + 1, false, "Użytkownik", QuizTitle, categories);
            foreach (var q in questions)
                quiz.AddQuestion(q);

            main.Materials.Add(quiz);
            Reset();
            await Shell.Current.GoToAsync("//MainPage");
        }

        private string? Validate()
        {
            if (string.IsNullOrWhiteSpace(QuizTitle))
                return "Podaj tytuł quizu.";

            for (int i = 0; i < questions.Count; i++)
            {
                var q = questions[i];
                if (string.IsNullOrWhiteSpace(q.Quest) || q.Answers.Any(string.IsNullOrWhiteSpace) || q.GoodAnswerIndex < 0)
                    return $"Pytanie {i + 1}: wpisz pytanie, wszystkie odpowiedzi i zaznacz prawidłową.";
            }
            return null;
        }

        private void Reset()
        {
            questions = new() { NewQuestion() };
            currentIndex = 0;
            QuizTitle = "";
            SelectedCategory = null;
            Description = "";
            Show();
        }
    }
}