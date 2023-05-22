using FluentValidation;
using SimpleStock.Application.Validators.CustomValidators;
using SimpleStock.Application.Validators.Shared;
using SimpleStock.Domain.DTOs.Order;

namespace SimpleStock.Application.Validators;

public class OrderCreateValidator : AbstractValidator<OrderRequestDto>
{
    public OrderCreateValidator()
    {
        // Emission date validation
        RuleFor(e => e.EmissionDate).NotEmpty().WithMessage("Data de emissão deve ser preenchida");

        // Customer Id validation
        RuleFor(e => e.CustomerId).NotEmpty().WithMessage("Id do cliente deve ser preenchido");

        //Addresses Validator
        RuleForEach(e => e.OrderItems).SetValidator(new OrderItemCreateValidator());
    }
}
