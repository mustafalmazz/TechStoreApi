using Microsoft.EntityFrameworkCore;
using TechStoreApi.Data;
using TechStoreApi.Models;
using TechStoreApi.Services.Abstract;

namespace TechStoreApi.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);
            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            return product;
        }

        public async Task UpdateProductAsync(int id, Product product)
        {
            var guncellenecek = await _context.Products.FirstOrDefaultAsync(f => f.Id == id);
            if (guncellenecek != null)
            {
                guncellenecek.Name = product.Name;
                guncellenecek.Price = product.Price;
                guncellenecek.Stock = product.Stock;
                guncellenecek.CategoryId = product.CategoryId;
                guncellenecek.Description = product.Description;
                await _context.SaveChangesAsync();
            }
        }
    }
}
