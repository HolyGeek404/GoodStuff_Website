using System.Net;
using Website.Models;
using Website.Models.User;
using Website.Services.Interfaces;

namespace Website.Api;

public class UserApiClient(
    HttpClient client,
    IConfiguration configuration,
    IRequestMessageBuilder requestMessageBuilder,
    ILogger<UserApiClient> logger)
{
    private readonly string _scope = configuration.GetSection("GoodStuffUserApi")["Scope"]!;

    public async Task<ApiResult> SignUpAsync(SignUpModel model)
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

    public async Task<ApiResult> SignInAsync(string email, string password)
    {
        var apiResult = new ApiResult();

        try
        {
            var request =
                await requestMessageBuilder.BuildGet(_scope, $"user/signin?email={email}&password={password}");
            var response = await client.SendAsync(request);

            apiResult.Success = response.IsSuccessStatusCode;
            apiResult.StatusCode = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                apiResult.Content = await response.Content.ReadFromJsonAsync<UserModel>();
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