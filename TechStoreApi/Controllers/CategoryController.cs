using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStoreApi.Data;
using TechStoreApi.Models;

namespace TechStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allCategories = await _context.Categories.Include(a => a.Products).ToListAsync();

            return Ok(allCategories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var one = await _context.Categories.Include(o => o.Products).FirstOrDefaultAsync(a => a.Id == id);
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
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("Id uyuşmadı");
            }
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(a => a.Id == id);
            if (existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.Name = category.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public  async Task<IActionResult> DeleteAsync(int id)
        {
            var forDelete = await _context.Categories.FindAsync(id);
            if (forDelete == null)
            {
                return NotFound("Kategori Bulunamadı");
            }
            _context.Categories.Remove(forDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    
    }
}
