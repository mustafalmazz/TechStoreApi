using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStoreApi.Data;
using TechStoreApi.Models;

namespace TechStoreApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Products.Include(p => p.Category).ToListAsync();

            return Ok(list);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.Include(f => f.Category).FirstOrDefaultAsync(a => a.Id == id);
            if (product == null)
            {
                return NotFound("Ürün Bulunamadı");
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetById),// 1. ADRES: "Bu veriyi nerede görebilirim?" -> GetById metodunda.
                new { id = product.Id },// 2. PARAMETRE: "O metoda gitmek için hangi ID lazım?" -> Yeni oluşan ID.
                product//Ürünün oluşan son halini döner.
                );
        }
        [HttpPut ("{id}")]
        public  async Task<IActionResult> Update(int id , Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("ID uyuşmazlığı.");
            }
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.CategoryId = product.CategoryId;

           await _context.SaveChangesAsync();

            return NoContent();//güncelleme yapanlar için 204 kodu döner.Veri döndürmez.Güncellenen veriyi genelde merak etmezler
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID Bulunamadı!");
            }
            var productToDelete = await _context.Products.FirstOrDefaultAsync(x=>x.Id == id);
            if (productToDelete == null)
            {
                return NotFound("Silinecek Ürün Bulunamadı!");
            }
            _context.Products.Remove(productToDelete);
           await _context.SaveChangesAsync();
            return NoContent();//Bir şeyi sildikten sonra geriye "Tamam sildim" (200 OK) demek yerine, "İşlem tamam, içerik yok" (204 No Content) demek standarttır.
        }
    }
}
