using System.Text.RegularExpressions;

namespace GoodStuff.Website.Domain.ValueObjects;

public static partial class PasswordRegex
{
    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
    public static partial Regex PasswordPattern();
}