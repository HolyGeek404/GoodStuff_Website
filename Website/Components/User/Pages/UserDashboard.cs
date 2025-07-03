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

    public void LogOut()
    {
        sessionService
    }
}