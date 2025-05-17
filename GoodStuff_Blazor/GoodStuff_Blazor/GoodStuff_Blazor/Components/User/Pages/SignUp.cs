using GoodStuff_Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace GoodStuff_Blazor.Components.User.Pages;

public partial class SignUp
{
    [SupplyParameterFromForm]
    private SignUpModel SignUpModel { get; set; } = new SignUpModel();

    protected override async Task OnInitializedAsync()
    {
        ApiClient.SignUpAsync(new SignUpModel()
        {
            Email = "email",
            Password = "password"
        });
        await Task.CompletedTask;
    }

    private async Task HandleValidSubmit()
    {
        var a = SignUpModel.Name;
        // Here you can handle the form submission, e.g., send the data to an API
        // For now, we'll just display a message
        await Task.Delay(1000); // Simulate a delay for the API call
        Console.WriteLine("Form submitted successfully!");
    }
}