using System.ComponentModel.DataAnnotations;
using GoodStuff.Website.Domain.ValueObjects;
using GoodStuff.Website.Domain.ValueObjects.Name;

namespace GoodStuff.Website.Presentation.Requests.Validators;

public class NameValidation : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string name) return NameRegex.NamePattern().IsMatch(name);
        return false;
    }
}