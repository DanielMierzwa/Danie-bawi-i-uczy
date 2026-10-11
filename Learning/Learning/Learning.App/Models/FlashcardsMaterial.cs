using Learning.App.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.App.Models
{
    public class FlashcardsMaterial : AbstractMaterial
    {
        // zamysł jest taki, że metody AddFlashcard(), GetFlashcardFromIndex(), GetFlashcardCount() i RemoveFlashcard()
        // są potrzebne tylko do edytora, a metody GetNextFlashcard() i ReturnFlashcard(),
        // viewmodel będzie wywoływał GetNextFlashcard() w try{}, dopóki w powtórkach nic nie zostanie
        private List<Flashcard> flashcards = new();
        private List<Flashcard> reviews = null;
        private int currentFlashCardIndex;

        public FlashcardsMaterial(int id, bool isPublic, string creatorLogin, string title, List<int> categoriesId) 
            : base(id, isPublic, creatorLogin, title, categoriesId) { currentFlashCardIndex = 0; }
        public void ReturnFlashcard(Flashcard f)
        {
            reviews.Insert(0, f);
        }
        public Flashcard GetNextFlashcard()
        {
            if (reviews == null)
                foreach (var card in flashcards)
                    reviews.Add(card.Copy());//tworzymy kopie żeby nie stracić fiszek i nie trzebaby ich jeszcze raz wczytywać z bazy
            if (reviews.Count == 0)
                throw new FlashcardsReviewEndedException();
            currentFlashCardIndex++;
            if (currentFlashCardIndex >= reviews.Count)
                currentFlashCardIndex = 0;
            var f = reviews[currentFlashCardIndex];
            reviews.Remove(f);
            return f;
        }

        public void AddFlashcard(Flashcard f)
        {
            flashcards.Add(f);
        }

        public void RemoveFlashcard(Flashcard f)
        {
            flashcards.Remove(f);
        }
        
        public Flashcard GetFlashcardFromIndex(int index)
        {
            return flashcards[index];
        }
        public int GetFlashcardCount()
        {
            return flashcards.Count;
        }
        public override void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
