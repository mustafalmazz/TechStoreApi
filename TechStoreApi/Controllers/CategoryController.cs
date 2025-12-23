using Microsoft.AspNetCore.Mvc;
using TechStoreApi.Models;
using TechStoreApi.Services.Abstract;

namespace TechStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allCategories = await _categoryService.GetAllCategories();

            return Ok(allCategories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var one = await _categoryService.GetCategoryById(id);
            if (one == null)
            {
                return NotFound("Kategori Bulunamadı");
            }
            return Ok(one);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            await _categoryService.AddCategory(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("Id uyuşmadı");
            }
            var existingCategory = await _categoryService.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            await _categoryService.UpdateCategory(id, category);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var forDelete = await _categoryService.GetCategoryById(id);
            if (forDelete == null)
            {
                return NotFound("Kategori Bulunamadı");
            }
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    
    }
}
