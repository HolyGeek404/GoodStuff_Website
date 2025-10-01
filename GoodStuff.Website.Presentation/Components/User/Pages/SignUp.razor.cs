using GoodStuff.Website.Domain.Models.User;
using GoodStuff.Website.Infrastructure.Api;
using Microsoft.AspNetCore.Components;

namespace GoodStuff.Website.Presentation.Components.User.Pages;

public partial class SignUp
{
    [SupplyParameterFromForm] private SignUpModel SignUpModel { get; } = new();
    [Inject] private UserApiClient ApiClient { get; set; }

    private async Task SignUpAsync()
    {
        var result = await ApiClient.SignUpAsync(SignUpModel);
        Console.WriteLine(result.Success
            ? "User signed up successfully"
            : $"Error {result.StatusCode} signing up user: {result.ErrorMessage}");
    }
}