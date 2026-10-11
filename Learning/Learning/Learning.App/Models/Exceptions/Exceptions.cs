using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.App.Models.Exceptions
{
    public class FlashcardsReviewEndedException : Exception
    {
        public FlashcardsReviewEndedException() : base("Zakończono powtórkę") { }   
    }

    public class GoodAnswerIndexOutOfRangeException : Exception
    {
        public GoodAnswerIndexOutOfRangeException(int index, int answersCount) : base($"GoodAnswerIndex został ustawiony na {index}, a pytań jest tylko {answersCount}") { }
    }

    public class DoubledCategoryException : Exception
    {
        public DoubledCategoryException(int id, string title) : base($"Materiał '{title}' posiada już kategorię o id:{id}") { }
    }
}
