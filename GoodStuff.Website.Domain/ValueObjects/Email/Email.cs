namespace GoodStuff.Website.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email required");
        if (!EmailRegex.EmailPattern().IsMatch(value)) throw new ArgumentException("Invalid email format");
        Value = value.Trim().ToLowerInvariant();
    }
}