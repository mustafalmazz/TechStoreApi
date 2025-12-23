using TechStoreApi.Models;

namespace TechStoreApi.Services.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task AddCategory(Category category);
        Task UpdateCategory(int id, Category category);
        Task DeleteCategory(int id);
    }
}
