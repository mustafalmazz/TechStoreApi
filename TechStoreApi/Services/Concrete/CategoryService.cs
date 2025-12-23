using Microsoft.EntityFrameworkCore;
using TechStoreApi.Data;
using TechStoreApi.Models;
using TechStoreApi.Services.Abstract;

namespace TechStoreApi.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var cToDelete = await _context.Categories.FindAsync(id);
            if (cToDelete != null)
            {
                _context.Categories.Remove(cToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category;
        }

        public async Task UpdateCategory(int id, Category category)
        {
            var find = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (find == null)
            {
                throw new Exception("Kategori bulunamadı");
            }
            find.Name = category.Name;
            await _context.SaveChangesAsync();
        }
    }
}
