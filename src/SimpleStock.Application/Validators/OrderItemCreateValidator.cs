using FluentValidation;
using SimpleStock.Domain.DTOs.OrderItem;

namespace SimpleStock.Application.Validators;

public class OrderItemCreateValidator : AbstractValidator<OrderItemCreateDto>
{
    public OrderItemCreateValidator()
    {
        // Product ID validation
        RuleFor(e => e.ProductId.ToString()).NotNull().NotEmpty().WithMessage("ID do produto deve ser preenchido");

        // Amount validation
        RuleFor(e => e.Amount).NotEmpty().WithMessage("Quantidade deve ser preenchida com um valor positivo e maior que 0");
    }
}
