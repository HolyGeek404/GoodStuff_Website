using System.ComponentModel.DataAnnotations;

namespace GoodStuff.Website.Presentation.Requests.Validators;

public class CheckboxValidation : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value is true;
    }
}