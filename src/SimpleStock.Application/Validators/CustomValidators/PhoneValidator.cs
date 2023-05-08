using FluentValidation;
using System.Text.RegularExpressions;

namespace SimpleStock.Application.Validators.Shared;
public static class PhoneValidator<T>
{
    public static void Validate(
        string phoneNumber, ValidationContext<T> context, string errorMessage)
    {
        if (!IsValidPhoneNumber(phoneNumber))
            context.AddFailure(new FluentValidation.Results.ValidationFailure(
                nameof(phoneNumber), errorMessage));
    }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        string phonePattern = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
        var validationResult = Regex.IsMatch(phoneNumber, phonePattern);

        return validationResult;
    }
}
