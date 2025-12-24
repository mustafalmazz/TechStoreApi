using FluentValidation;
using TechStoreApi.Models;

namespace TechStoreApi.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün adı boş geçilemez.") 
                .NotNull().WithMessage("Ürün adı girilmek zorundadır.") 
                .MaximumLength(200).WithMessage("Ürün adı en fazla 200 karakter olabilir.") 
                .MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalıdır.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stok adedi negatif olamaz."); 

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır."); 

            RuleFor(x => x.CategoryId)
                 .GreaterThan(0).WithMessage("Lütfen geçerli bir kategori seçiniz.");
        }
    }
}