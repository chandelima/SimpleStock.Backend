using FluentValidation;
using System.Text.RegularExpressions;

namespace SimpleStock.Application.Validators.Shared;
public static class PostalCodeValidator<T>
{
    public static void Validate(
        string postalCode, ValidationContext<T> context, string errorMessage)
    {
        if (!IsValidPostalCode(postalCode))
            context.AddFailure(new FluentValidation.Results.ValidationFailure(
                nameof(postalCode), errorMessage));
    }

    private static bool IsValidPostalCode(string postalCode)
    {
        string postalCodePattern = "^\\d{2}\\d{3}[-]\\d{3}$";
        var validationResult = Regex.IsMatch(postalCode, postalCodePattern);

        return validationResult;
    }
}
