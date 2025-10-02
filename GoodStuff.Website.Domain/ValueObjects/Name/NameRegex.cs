using System.Text.RegularExpressions;

namespace GoodStuff.Website.Domain.ValueObjects;

public static partial class NameRegex
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    public static partial Regex NamePattern();
}