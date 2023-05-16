using FluentValidation;
using SimpleStock.Application.Validators.Shared;
using SimpleStock.Domain.DTOs.Address;
using SimpleStock.Domain.DTOs.Customer;

namespace SimpleStock.Application.Validators;
public class AddressCreateValidator : AbstractValidator<AddressCreateRequestDto>
{
    public AddressCreateValidator()
    {
        // StreetName validation
        RuleFor(e => e.StreetName).NotEmpty().WithMessage("Logradouro deve ser preenchido");
        When(e => !string.IsNullOrWhiteSpace(e.StreetName), () =>
        {
            RuleFor(e => e.StreetName)
                .MinimumLength(3).WithMessage("Logradouro deve ter pelo menos 3 caractéres")
                .MaximumLength(50).WithMessage("Logradouro deve ter no máximo 50 caractéres");
        });

        // Number validation
        RuleFor(e => e.Number).NotEmpty().WithMessage("Número deve ser preenchido");
        When(e => !string.IsNullOrWhiteSpace(e.Number), () =>
        {
            RuleFor(e => e.Number)
                .MaximumLength(16).WithMessage("Número deve ter no máximo 16 caractéres");
        });

        // Neighborhood validation
        RuleFor(e => e.Neighborhood).NotEmpty().WithMessage("Bairro deve ser preenchido");
        When(e => !string.IsNullOrWhiteSpace(e.Neighborhood), () =>
        {
            RuleFor(e => e.Neighborhood)
                .MinimumLength(3).WithMessage("Bairro ter pelo menos 3 caractéres")
                .MaximumLength(50).WithMessage("Bairro ter no máximo 50 caractéres");
        });

        // City validation
        RuleFor(e => e.Neighborhood).NotEmpty().WithMessage("Cidade deve ser preenchido");
        When(e => !string.IsNullOrWhiteSpace(e.Neighborhood), () =>
        {
            RuleFor(e => e.Neighborhood)
                .MinimumLength(3).WithMessage("Cidade deve ter pelo menos 3 caractéres")
                .MaximumLength(30).WithMessage("Cidade deve ter no máximo 30 caractéres");
        });

        // State validation
        RuleFor(e => e.State).NotEmpty().WithMessage("Estado deve ser preenchido");
        When(e => !string.IsNullOrWhiteSpace(e.State), () =>
        {
            RuleFor(e => e.State)
                .MinimumLength(3).WithMessage("Estado deve ter pelo menos 3 caractéres")
                .MaximumLength(30).WithMessage("Estado deve ter no máximo 30 caractéres");
        });

        // PostalCode validation
        RuleFor(e => e.PostalCode).NotEmpty().WithMessage("Estado deve ser preenchido");
        When(e => !string.IsNullOrWhiteSpace(e.PostalCode), () =>
        {
            RuleFor(e => e.PostalCode).Custom(
                (phoneNumber, context) => PostalCodeValidator<AddressCreateRequestDto>
                    .Validate(phoneNumber, context, "CEP deve estar no formato \"00000-000\""));
        });
    }
}
