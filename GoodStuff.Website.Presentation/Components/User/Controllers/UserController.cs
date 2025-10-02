using GoodStuff.Website.Application.Services.Interfaces;
using GoodStuff.Website.Domain.Entities.User;
using GoodStuff.Website.Domain.ValueObjects;
using GoodStuff.Website.Infrastructure.Api;
using GoodStuff.Website.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GoodStuff.Website.Presentation.Components.User.Controllers;

[Route("{controller}")]
public class UserController(
    UserApiClient userApiClient,
    IUserSessionService userSessionService,
    ILogger<UserController> logger) : ControllerBase
{
    [HttpPost]
    [Route("signin")]
    public async Task<IActionResult> SignIn([FromForm] SignInRequest request)
    {
        logger.LogInformation($"Creating session for user {request.Email}.");
        try
        {
            var email = new Email(request.Email);
            var password = new Password(request.Password);
            var result = await userApiClient.SignInAsync(email, password);
            var userModel = (Domain.Entities.User.User)result.Content;
            if (!result.Success) return BadRequest();

            var sessionId = userSessionService.CreateSession(userModel);
            if (string.IsNullOrEmpty(sessionId)) return BadRequest();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                MaxAge = TimeSpan.FromMinutes(10)
            };
            Response.Cookies.Append("UserSessionId", sessionId, cookieOptions);
            logger.LogInformation($"Created session for user {userModel.Email}.");

            return Redirect("/user/dashboard");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error signing in user {request.Email}. Error: {ex.Message}");
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("{signout}")]
    public IActionResult SignOut()
    {
        try
        {
            var sessionId = Request.Cookies["UserSessionId"];
            userSessionService.ClearUserCachedData(sessionId);
            Response.Cookies.Delete("UserSessionId");
            return Redirect("/");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error signing out user {Request.Cookies["UserSessionId"]}");
            return BadRequest();
        }
    }
}