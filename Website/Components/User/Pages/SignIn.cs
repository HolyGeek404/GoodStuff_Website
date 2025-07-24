using Microsoft.AspNetCore.Components;
using Website.Models.User;

namespace Website.Components.User.Pages;

public partial class SignIn
{
    [Inject] private HttpClient Client { get; set; }
    [Inject] private NavigationManager Navigation { get; set; }
    [SupplyParameterFromForm] private SignInModel SignInModel { get; set; } = new();
    private string? _errorMessage;
    private async Task SignInAsync()
    {
        var result = await Client.PostAsJsonAsync($"https://localhost:7001/user/signin", SignInModel);
        
        if (result.IsSuccessStatusCode)
        {
            Navigation.NavigateTo("/user/dashboard");
        }
        else
        {
            _errorMessage = "Something went wrong";
        }
    }
}