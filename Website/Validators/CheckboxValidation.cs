using System.ComponentModel.DataAnnotations;

namespace Website.Validators;

public class CheckboxValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is true;
    }
}