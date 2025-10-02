using System.ComponentModel.DataAnnotations;
using GoodStuff.Website.Domain.ValueObjects;

namespace GoodStuff.Website.Presentation.Requests.Validators;

public class PasswordValidation : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string password) return PasswordRegex.PasswordPattern().IsMatch(password);
        return false;
    }
}