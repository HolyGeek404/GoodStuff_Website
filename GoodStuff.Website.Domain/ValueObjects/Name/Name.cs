namespace GoodStuff.Website.Domain.ValueObjects;

public sealed record Name
{
    public string Value { get; }
    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name required");
        if (!NameRegex.NamePattern().IsMatch(value)) throw new ArgumentException("Name contains invalid characters");
        Value = value.Trim();
    }
}