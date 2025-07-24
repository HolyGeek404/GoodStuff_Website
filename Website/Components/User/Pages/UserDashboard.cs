using Microsoft.AspNetCore.Components;
using Website.Models.User;
using Website.Services.Interfaces;

namespace Website.Components.User.Pages;

public partial class UserDashboard
{
    [Inject]
    private NavigationManager Navigation { get; set; }

    [Inject]
    private IUserSessionService SessionService { get; set; }

    private UserSession UserSession { get; set; }
    private string? ErrorMessage { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        // if (SessionService.Validate())
        // {
        //     UserSession = SessionService.GetUserSession()!;
        // }
        // else
        // {
        //     Navigation.NavigateTo("/sign-in");
        // }
    }

    private void SignOut()
    {
        try
        {
            SessionService.SignOut();
            Navigation.NavigateTo("/sign-in");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Couldn't sign out because: {ex}";
        }
    }
}