using GoodStuff_Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace GoodStuff_Blazor.Components.User.Pages;

public partial class SignIn
{
    [SupplyParameterFromForm]
    private SignInModel SignInModel { get; set; } = new SignInModel();

    private async Task SignInAsync()
    {
        var result = await ApiClient.SignInAsync(SignInModel.Email, SignInModel.Password);
        Console.WriteLine(result.Success
            ? "User signed in successfully"
            : $"Error {result.StatusCode} signing in user: {result.ErrorMessage}");
        if (result.Success)
        {
            NavigationManager.NavigateTo("/user/dashboard");
        }
    }
}