using Microsoft.AspNetCore.Components;

namespace GoodStuff_Blazor.Components.User.Pages;

public partial class SignIn 
{
    [SupplyParameterFromForm]
    private SignInModel SignInModel { get; set; } = new SignInModel();

    private async Task HandleValidSubmit()
    {
        // Handle the form submission
        await Task.Delay(1000); // Simulate a delay for the API call

        Console.WriteLine($"Email: {SignInModel.Email}, Password: {SignInModel.Password}");
    }
}