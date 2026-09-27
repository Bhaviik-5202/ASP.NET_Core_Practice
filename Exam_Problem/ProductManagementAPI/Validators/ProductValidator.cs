using FluentValidation;
using ProductManagementAPI.Models;

namespace ProductManagementAPI.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MaximumLength(100)
                .WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(p => p.ProductCode)
                .NotEmpty()
                .WithMessage("Product code is required.")
                .MaximumLength(50)
                .WithMessage("Product code cannot exceed 50 characters.");

            RuleFor(p => p.Category)
                .NotEmpty()
                .WithMessage("Category is required.");

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

            RuleFor(p => p.StockQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock quantity cannot be negative.");

            RuleFor(p => p.ManufactureDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Manufacture date cannot be in the future.");

            RuleFor(p => p.ExpiryDate)
                .GreaterThan(p => p.ManufactureDate)
                .WithMessage("Expiry date must be after manufacture date.");
        }
    }
}
