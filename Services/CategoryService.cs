using IPDP_Stefan.Context;
using IPDP_Stefan.Interfaces;
using IPDP_Stefan.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPDP_Stefan.Services
{
    public class CategoryService : ICategoryService
    {
        private Context.ContextDb _context;

        public CategoryService(ContextDb context)
        {
            _context = context;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
            return category;
        }

        public async Task DeleteCategory(Category category)
        {
            _context.Category.Remove(category);
            _context.SaveChanges();
        }
        public async Task<Category> EditCategory(Category category)
        {
            var existingCategory = _context.Category.Find(category.Id); 
            if(existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.Parent_category = category.Parent_category;

                _context.Category.Update(existingCategory);
                _context.SaveChanges();
            }
            return category;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return _context.Category.SingleOrDefault(x => x.Id == id);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return _context.Category.SingleOrDefault(x => x.Name == name);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return _context.Category.ToList();
        }

    }
}
