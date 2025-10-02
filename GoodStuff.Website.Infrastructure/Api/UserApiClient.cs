using System.Net;
using System.Net.Http.Json;
using GoodStuff.Website.Application.Services.Interfaces;
using GoodStuff.Website.Domain.Entities;
using GoodStuff.Website.Domain.Entities.User;
using GoodStuff.Website.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GoodStuff.Website.Infrastructure.Api;

public class UserApiClient(
    HttpClient client,
    IConfiguration configuration,
    IRequestMessageBuilder requestMessageBuilder,
    ILogger<UserApiClient> logger)
{
    private readonly string _scope = configuration.GetSection("GoodStuffUserApi")["Scope"]!;

    public async Task<ApiResult> SignUpAsync(User model)
    {
        var apiResult = new ApiResult();

        try
        {
            var request = await requestMessageBuilder.BuildPost(_scope, "user/signup", model);
            var response = await client.SendAsync(request);

            apiResult.Success = response.IsSuccessStatusCode;
            apiResult.StatusCode = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                apiResult.Content = await response.Content.ReadFromJsonAsync<bool>();
                apiResult.Success = true;
            }
            else
            {
                apiResult.ErrorMessage = await response.Content.ReadAsStringAsync();
                apiResult.Success = false;
                logger.LogError(
                    $"Couldn't SignUp. Http Code: {response.StatusCode}. Error Message: {apiResult.ErrorMessage}");
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Couldn't sign up user {model.Email}. Error: {e.Message}");
            apiResult.Success = false;
            apiResult.ErrorMessage = "An error unexpected occurred while signing up";
            apiResult.StatusCode = HttpStatusCode.InternalServerError;
        }

        return apiResult;
    }

    public async Task<ApiResult> SignInAsync(Email email, Password password)
    {
        var apiResult = new ApiResult();

        try
        {
            var request =
                await requestMessageBuilder.BuildGet(_scope, $"user/signin?email={email.Value}&password={password.Value}");
            var response = await client.SendAsync(request);

            apiResult.Success = response.IsSuccessStatusCode;
            apiResult.StatusCode = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                apiResult.Content = await response.Content.ReadFromJsonAsync<User>();
                apiResult.Success = response.IsSuccessStatusCode;
            }
            else
            {
                apiResult.ErrorMessage = await response.Content.ReadAsStringAsync();
                apiResult.Success = false;
                logger.LogError(
                    $"Couldn't SignIn. Http Code: {response.StatusCode}. Error Message: {apiResult.ErrorMessage}");
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Couldn't sign iun user {email}. Error: {e.Message}");
            apiResult.Success = false;
            apiResult.ErrorMessage = "An error unexpected occurred while signing in";
            apiResult.StatusCode = HttpStatusCode.InternalServerError;
        }

        return apiResult;
    }
}