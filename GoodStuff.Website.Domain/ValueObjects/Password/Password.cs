namespace GoodStuff.Website.Domain.ValueObjects;

public sealed record Password
{
    public string Value { get; }
    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name required");
        if (!PasswordRegex.PasswordPattern().IsMatch(value)) throw new ArgumentException("Name contains invalid characters");
        Value = value.Trim();
    }
}