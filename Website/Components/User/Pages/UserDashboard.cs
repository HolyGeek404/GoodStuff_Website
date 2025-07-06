using Microsoft.AspNetCore.Components;
using Website.Models.User;
using Website.Services.Interfaces;

namespace Website.Components.User.Pages;

public partial class UserDashboard
{
    [Inject]
    private NavigationManager navigation { get; set; }

    [Inject]
    private IUserSessionService sessionService { get; set; }

    private UserSession userSession { get; set; }
    private string errorMessage { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        if (sessionService.Validate())
        {
            userSession = sessionService.GetUserSession()!;
        }
        else
        {
            navigation.NavigateTo("/sign-in");
        }
    }

    private void SignOut()
    {
        try
        {
            sessionService.SignOut();
            navigation.NavigateTo("/sign-in");
        }
        catch (Exception ex)
        {
            errorMessage = $"Couldn't sign out because: {ex}";
        }
    }
}