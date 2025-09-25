using GoodStuff.Application.Services.Interfaces;
using GoodStuff.Domain.Models.User;
using Microsoft.AspNetCore.Components;

namespace GoodStuff.Web.Components.User.Pages;

public partial class UserDashboard
{
    [Inject] private NavigationManager Navigation { get; set; }

    [Inject] private IUserSessionService SessionService { get; set; }

    private UserSession UserSession { get; set; }
    private string? ErrorMessage { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        if (SessionService.Validate())
            UserSession = SessionService.GetUserSession()!;
        else
            Navigation.NavigateTo("/sign-in");
    }
}