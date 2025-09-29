using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GoodStuff.Domain.Validators;

public class PasswordValidation : ValidationAttribute
{
    private const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

    public override bool IsValid(object? value)
    {
        if (value is string password) return Regex.IsMatch(password, PasswordPattern);
        return false;
    }
}