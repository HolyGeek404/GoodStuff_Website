namespace GoodStuff.Website.Domain.Entities.User;

using GoodStuff.Website.Domain.ValueObjects;

public class User(Name name, Name surname, Email email, Password password)
{
    public int Id { get; init; }
    public Name Name { get; init; } = name;
    public Name Surname { get; init; } = surname;
    public Email Email { get; init; } = email;
    public Password Password { get; init; } = password;
}