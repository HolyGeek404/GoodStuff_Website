using System.Security.Authentication;
using GoodStuff.Website.Application.Services.Interfaces;
using GoodStuff.Website.Domain.ValueObjects;
using GoodStuff.Website.Domain.ValueObjects.Email;
using GoodStuff.Website.Domain.ValueObjects.Password;
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
        if (!ModelState.IsValid) return BadRequest(ModelState);
        logger.LogInformation("Creating session for user {RequestEmail}.", request.Email);

        var email = new Email(request.Email);
        var password = new Password(request.Password);
        try
        {
            var user = await userApiClient.SignInAsync(email, password);

            var sessionId = userSessionService.CreateSession(user);
            if (string.IsNullOrEmpty(sessionId)) return BadRequest();

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, MaxAge = TimeSpan.FromMinutes(10)
            };
            Response.Cookies.Append("UserSessionId", sessionId, cookieOptions);
            logger.LogInformation("Created session for user {EmailValue}.", user.Email.Value);

            return Redirect("/user/dashboard");
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Network or API error signing in user {Email}.", request.Email);
            return StatusCode(StatusCodes.Status502BadGateway);
        }
        catch (AuthenticationException ex)
        {
            logger.LogWarning(ex, "Invalid credentials for {Email}.", request.Email);
            return Unauthorized();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error signing in user {Email}.", request.Email);
            return StatusCode(StatusCodes.Status500InternalServerError);
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
            logger.LogError(ex, "Error signing out user {RequestCookie}", Request.Cookies["UserSessionId"]);
            return BadRequest();
        }
    }
}