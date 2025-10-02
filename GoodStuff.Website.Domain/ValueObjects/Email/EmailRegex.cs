using System.Text.RegularExpressions;

namespace GoodStuff.Website.Domain.ValueObjects;

public static partial class EmailRegex
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    public static partial Regex EmailPattern();
}