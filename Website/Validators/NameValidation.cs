using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Website.Validators;

public class NameValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is string name) return Regex.IsMatch(name, @"^[a-zA-Z]{3,}$");
        return false;
    }
}