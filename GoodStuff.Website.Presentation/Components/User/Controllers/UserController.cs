using GoodStuff.Website.Application.Services.Interfaces;
using GoodStuff.Website.Domain.Models.User;
using GoodStuff.Website.Infrastructure.Api;
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
    public async Task<IActionResult> SignIn([FromForm] SignInModel model)
    {
        logger.LogInformation($"Creating session for user {model.Email}.");
        try
        {
            var result = await userApiClient.SignInAsync(model.Email, model.Password);
            var userModel = (UserModel)result.Content;
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
            logger.LogError(ex, $"Error signing in user {model.Email}. Error: {ex.Message}");
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