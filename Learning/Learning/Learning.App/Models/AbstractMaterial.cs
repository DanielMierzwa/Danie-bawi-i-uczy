using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.App.Models
{
    public abstract class AbstractMaterial
    {
        protected int id;
        public bool IsPublic { get; protected set; }
        public string CreatorLogin { get; protected set; }
        public string Title { get; protected set; }
        public DateTime CreationData { get; protected set; }
        public List<int> CategoriesId { get; protected set; }

        public AbstractMaterial(int id, bool isPublic, string creatorLogin, string title, List<int> categoriesId)
        {
            this.id = id;
            IsPublic = isPublic;
            CreatorLogin = creatorLogin;
            Title = title;
            CreationData = DateTime.Now;
            CategoriesId = categoriesId;
        }

        public abstract void Delete();
        public void SetVisibility(bool isPublic)
        {
            IsPublic = isPublic;
        }

        public void AddCategoryId(int categoryId)
        {
            if (!CategoriesId.Contains(categoryId))
                CategoriesId.Add(categoryId);
            else
                throw new Exception($"Materiał: {Title} posiada już kategorię o id: {categoryId}");
        }
        public void ResetCategories()
        {
            CategoriesId = new List<int>();
        }
    }
}
