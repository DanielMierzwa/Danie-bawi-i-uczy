using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.App.Models
{
    public class Flashcard
    {
        public string Top;
        public string Bottom;

        public Flashcard Copy()
        {
            return new Flashcard() { Bottom = this.Bottom, Top = this.Top };
        }
    }
}
