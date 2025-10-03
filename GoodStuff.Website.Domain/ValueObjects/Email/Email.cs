namespace GoodStuff.Website.Domain.ValueObjects.Email;

public sealed record Email
{
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email required");
        if (!EmailRegex.EmailPattern().IsMatch(value)) throw new ArgumentException("Invalid email format");
        Value = value.Trim().ToLowerInvariant();
    }

    public string Value { get; }
}