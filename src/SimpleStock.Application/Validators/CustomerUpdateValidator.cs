using FluentValidation;
using SimpleStock.Application.Validators.CustomValidators;
using SimpleStock.Application.Validators.Shared;
using SimpleStock.Domain.DTOs.Customer;

namespace SimpleStock.Application.Validators;

public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateRequestDto>
{
    public CustomerUpdateValidator()
    {
        // Name validation
        RuleFor(e => e.Name).NotEmpty().WithMessage("Nome deve ser preenchido");
        When(e => !string.IsNullOrWhiteSpace(e.Name), () =>
        {
            RuleFor(e => e.Name)
                .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caractéres")
                .MaximumLength(50).WithMessage("Nome deve ter no máximo 50 caractéres");
        });

        // E-mail validation
        RuleFor(e => e.Email).NotEmpty().WithMessage("E-mail deve ser preenchido");
        When(e => !string.IsNullOrWhiteSpace(e.Email), () =>
        {
            RuleFor(e => e.Email)
                .EmailAddress().WithMessage("E-mail deve ser válido");
        });

        // Phone validation
        RuleFor(e => e.PhoneNumber).NotEmpty().WithMessage("Telefone deve ser preenchido");
        When(e => !string.IsNullOrWhiteSpace(e.PhoneNumber), () =>
        {
            RuleFor(e => e.PhoneNumber).Custom(
                (phoneNumber, context) => PhoneValidator<CustomerUpdateRequestDto>
                    .Validate(phoneNumber, context, "Telefone deve ser válido"));
        });

        // CPF validation
        RuleFor(e => e.Cpf).NotEmpty().WithMessage("CPF deve ser preenchido");
        When(e => !string.IsNullOrWhiteSpace(e.Cpf), () =>
        {
            RuleFor(e => e.Cpf).Custom(
                (phoneNumber, context) => CpfValidator<CustomerUpdateRequestDto>
                    .Validate(phoneNumber, context, "CPF deve ser válido"));
        });

        // Birthdate validation
        RuleFor(e => e.BirthDate).NotEmpty().WithMessage("Data de nascimento deve ser preenchida");
        When(e => !string.IsNullOrWhiteSpace(e.Cpf), () =>
        {
            RuleFor(e => e.BirthDate).LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Data de nascimento não pode ser superior a data atual");
        });
    }
}
