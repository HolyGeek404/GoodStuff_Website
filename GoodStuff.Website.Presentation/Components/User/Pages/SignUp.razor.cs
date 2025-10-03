using GoodStuff.Website.Domain.ValueObjects;
using GoodStuff.Website.Domain.ValueObjects.Email;
using GoodStuff.Website.Domain.ValueObjects.Name;
using GoodStuff.Website.Domain.ValueObjects.Password;
using GoodStuff.Website.Infrastructure.Api;
using GoodStuff.Website.Presentation.Requests;
using Microsoft.AspNetCore.Components;

namespace GoodStuff.Website.Presentation.Components.User.Pages;

public partial class SignUp
{
    [SupplyParameterFromForm] private SignUpRequest SignUpRequest { get; } = new();
    [Inject] private UserApiClient ApiClient { get; set; }

    private async Task SignUpAsync()
    {
        var name = new Name(SignUpRequest.Name);
        var surname = new Name(SignUpRequest.Surname);
        var email = new Email(SignUpRequest.Email);
        var password = new Password(SignUpRequest.Password);

        var user = new Domain.Entities.User.User(name, surname, email, password);
        var result = await ApiClient.SignUpAsync(user);
    }
}