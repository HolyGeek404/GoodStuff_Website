using GoodStuff.Website.Domain.ValueObjects;
using GoodStuff.Website.Domain.ValueObjects.Email;
using GoodStuff.Website.Domain.ValueObjects.Name;
using GoodStuff.Website.Domain.ValueObjects.Password;

namespace GoodStuff.Website.Domain.Entities.User;

public class User(Name name, Name surname, Email email, Password password)
{
    public int Id { get; init; }
    public Name Name { get; init; } = name;
    public Name Surname { get; init; } = surname;
    public Email Email { get; init; } = email;
    public Password Password { get; init; } = password;
}