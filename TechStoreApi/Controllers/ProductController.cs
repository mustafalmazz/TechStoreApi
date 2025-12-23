using Microsoft.AspNetCore.Mvc;
using TechStoreApi.Models;
using TechStoreApi.Services.Abstract;

namespace TechStoreApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _productService.GetAllProductsAsync();

            return Ok(list);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
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
            await _productService.AddProductAsync(product);
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
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            await _productService.UpdateProductAsync(id, product);

            return NoContent();//güncelleme yapanlar için 204 kodu döner.Veri döndürmez.Güncellenen veriyi genelde merak etmezler
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productToDelete = await _productService.GetProductByIdAsync(id);
            if (productToDelete == null)
            {
                return NotFound("Silinecek Ürün Bulunamadı!");
            }
           await _productService.DeleteProductAsync(id);
            return NoContent();//Bir şeyi sildikten sonra geriye "Tamam sildim" (200 OK) demek yerine, "İşlem tamam, içerik yok" (204 No Content) demek standarttır.
        }
    }
}
