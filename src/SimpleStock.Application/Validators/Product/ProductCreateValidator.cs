using FluentValidation;
using SimpleStock.Domain.DTOs.Product;

namespace SimpleStock.Application.Validators.Product;

public class ProductCreateValidator : AbstractValidator<ProductRequestDto>
{
    public ProductCreateValidator()
    {
        // Name validation
        RuleFor(e => e.Name).NotEmpty().WithMessage("Nome deve ser preenchido.");
        When(e => !string.IsNullOrWhiteSpace(e.Name), () =>
        {
            RuleFor(e => e.Name)
                .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caractéres")
                .MaximumLength(50).WithMessage("Nome deve ter no máximo 50 caractéres");
        });

        // Amount validation
        RuleFor(e => e.Amount).NotEmpty().WithMessage("Quantidade deve ser preenchido");
        When(e => e.Amount != 0m && e.Amount < 0.01m, () =>
        {
            RuleFor(e => e.Amount).GreaterThan(0.01m).WithMessage("Quantidade deve receber um valor positivo");
        });
        

        // Price validation
        RuleFor(e => e.Price).NotEmpty().WithMessage("Preço deve ser preenchido");
        When(e => e.Price != 0m && e.Price < 0.01m, () =>
        {
            RuleFor(e => e.Price).GreaterThan(0.01m).WithMessage("Preço deve receber um valor positivo");
        });
    }
}
