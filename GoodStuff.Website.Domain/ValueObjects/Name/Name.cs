namespace GoodStuff.Website.Domain.ValueObjects.Name;

public sealed record Name
{
    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name required");
        if (!NameRegex.NamePattern().IsMatch(value)) throw new ArgumentException("Name contains invalid characters");
        Value = value.Trim();
    }

    public string Value { get; }
}