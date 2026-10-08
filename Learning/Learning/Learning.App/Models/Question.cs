using Learning.App.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.App.Models
{
    public class Question
    {
        public string Quest { get; private set; }
        public string[] Answers { get; private set; }
        public int GoodAnswerIndex { get; private set; }

        public Question(string question, string[] answers, int goodAnswerIndex)
        {
            Quest = question;
            Answers = answers;
            if (goodAnswerIndex >= answers.Count())
                throw new GoodAnswerIndexOutOfRangeException(goodAnswerIndex, answers.Count());
            GoodAnswerIndex = goodAnswerIndex;
        }
    }
}
