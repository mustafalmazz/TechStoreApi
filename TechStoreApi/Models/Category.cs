namespace TechStoreApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public List<Product>? Products { get; set; }
    }
}
