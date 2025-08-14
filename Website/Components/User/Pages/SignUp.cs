using Microsoft.AspNetCore.Components;
using Website.Models.User;

namespace Website.Components.User.Pages;

public partial class SignUp
{
    [SupplyParameterFromForm]
    private SignUpModel SignUpModel { get; set; } = new SignUpModel();

    private async Task SignUpAsync()
    {
        var result = await ApiClient.SignUpAsync(SignUpModel);
        Console.WriteLine(result.Success ? "User signed up successfully" : $"Error {result.StatusCode} signing up user: {result.ErrorMessage}");
    }
}