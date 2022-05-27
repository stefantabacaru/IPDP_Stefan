using IPDP_Stefan.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPDP_Stefan.Interfaces
{
    public interface ICategoryService
    {
        public Task<Category> AddCategory(Category category);
        public Task DeleteCategory(Category category);
        public Task<Category> EditCategory(Category category);
        public Task<Category> GetCategoryById(int id);
        public Task<Category> GetCategoryByName(string name);
        public Task<List<Category>> GetAllCategories();
    }
}
