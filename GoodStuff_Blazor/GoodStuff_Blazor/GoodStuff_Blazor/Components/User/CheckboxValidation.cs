using System.ComponentModel.DataAnnotations;

namespace GoodStuff_Blazor.Components.User;

public class CheckboxValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is true;
    }
}