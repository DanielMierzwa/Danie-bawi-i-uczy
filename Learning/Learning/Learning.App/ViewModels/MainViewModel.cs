using CommunityToolkit.Mvvm.ComponentModel;
using Learning.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public ObservableCollection<AbstractMaterial> Materials { get; set; } = new();
        public MainViewModel()
        {
            Materials.Add(new QuizMaterial(1, true, "Anatol", "Quiz 1", new List<int>() { 1, 2 }));
            Materials.Add(new QuizMaterial(2, false, "Mietek", "Quiz 2", new List<int>() { 1 }));
            Materials.Add(new FlashcardsMaterial(3, true, "Staś", "Fiszki 1", new List<int>() { 2 }));
            Materials.Add(new FlashcardsMaterial(4, false, "Jerzy", "Fiszki 2", new List<int>() { 1, 2 }));
        }
    }
}
