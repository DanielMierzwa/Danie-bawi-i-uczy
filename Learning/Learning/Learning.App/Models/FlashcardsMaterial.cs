using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.App.Models
{
    public class FlashcardsMaterial : AbstractMaterial
    {
        private List<Flashcard> flashcards;

        public FlashcardsMaterial(int id, bool isPublic, string creatorLogin, string title, List<int> categoriesId) : base(id, isPublic, creatorLogin, title, categoriesId)
        {
        }

        public void AddFlashcard(Flashcard f)
        {
            flashcards.Add(f);
        }

        public Flashcard GetFlashcard(int FlashcardId)
        {
            return flashcards[FlashcardId];
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
