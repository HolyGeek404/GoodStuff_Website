using System.Net;
using GoodStuff_Blazor.Models;
using GoodStuff_Blazor.Services.Interfaces;
using Microsoft.Identity.Client;

namespace GoodStuff_Blazor.Api;

public class GoodStuffApiClient(HttpClient client, IConfiguration configuration, IRequestMessageBuilder requestMessageBuilder)
{
    public async Task<ApiResult> SignInAsync(SignInModel model)
    {
        ApiResult apiResult;
        try
        {
            var token = await GetAccessToken();
            var request = requestMessageBuilder.BuildPost(token, "user/signin", model);
            var response = await client.SendAsync(request);

            apiResult = response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => new ApiResult
                {
                    Success = false,
                    ErrorMessage = "Invalid credentials",
                    StatusCode = response.StatusCode
                },

                HttpStatusCode.Created => new ApiResult
                {
                    Success = true, 
                    StatusCode = response.StatusCode
                },

                _ => new ApiResult
                {
                    Success = false,
                    ErrorMessage = "An error unexpected occurred while signing in",
                    StatusCode = response.StatusCode
                }
            };
        }
        catch (Exception e)
        {
            // in the future, this will be replaced with a logger
            Console.WriteLine($"Couldn't SingIn user {model.Email}. Error: {e.Message}");
            apiResult = new ApiResult
            {
                Success = false,
                ErrorMessage = "An error unexpected occurred while signing in",
                StatusCode = HttpStatusCode.InternalServerError
            };
        }

        return apiResult;
    }
    //public async Task<bool> SignUpAsync(SignUpModel model)
    //{
    //    try
    //    {
    //        var token = await GetAccessToken();
    //        var request = requestMessageBuilder.BuildPost(token, "user/signup", model);

    //        var response = await client.SendAsync(request);
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine($"Couldn't SingIn user {model.Email}. Error: {e.Message}");
    //    }

    //    return response.StatusCode switch
    //    {
    //        System.Net.HttpStatusCode.Conflict => false,
    //        System.Net.HttpStatusCode.InternalServerError => throw new Exception("Internal server error"),
    //        _ => response.IsSuccessStatusCode
    //    };
    //}

    private async Task<string> GetAccessToken()
    {
        var azure = configuration.GetSection("AzureAd");
        var tenantId = azure["TenantId"];
        var clientId = azure["ClientId"];
        var clientSecret = azure["ClientSecret"];
        var authority = $"https://login.microsoftonline.com/{tenantId}";
        var scope = azure["GoodStuffApiScope"];

        var app = ConfidentialClientApplicationBuilder.Create(clientId)
            .WithClientSecret(clientSecret)
            .WithAuthority(new Uri(authority))
            .Build();

        var result = await app.AcquireTokenForClient([scope]).ExecuteAsync();

        return result.AccessToken;
    }
}