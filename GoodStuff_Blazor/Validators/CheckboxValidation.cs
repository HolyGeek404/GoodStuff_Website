using System.ComponentModel.DataAnnotations;

namespace GoodStuff_Blazor.Validators;

public class CheckboxValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is true;
    }
}