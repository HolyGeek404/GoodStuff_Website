namespace GoodStuff.Website.Domain.ValueObjects.Password;

public sealed record Password
{
    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name required");
        if (!PasswordRegex.PasswordPattern().IsMatch(value))
            throw new ArgumentException("Name contains invalid characters");
        Value = value.Trim();
    }

    public string Value { get; }
}