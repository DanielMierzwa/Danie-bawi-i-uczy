using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.App.Models
{
    public class QuizMaterial : AbstractMaterial
    {
        private List<Question> questions  = new();
        public QuizMaterial(int id, bool isPublic, string creatorLogin, string title, List<int> categoriesId)
            : base(id, isPublic, creatorLogin, title, categoriesId) { }

        public void AddQuestion(Question q)
        {
            questions.Add(q);
        }

        public void RemoveQuestion(Question q)
        {
            questions.Remove(q);
        }

        public Question GetQuestionFromIndex(int index)
        {
            return questions[index];
        }

        public int GetQuestionCount()
        {
            return questions.Count();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
