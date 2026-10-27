using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.App.Models
{
    public class Flashcard
    {
        public string Front;
        public string Back;

        public Flashcard Copy()
        {
            return new Flashcard() { Back = this.Back, Front = this.Front };
        }
    }
}
