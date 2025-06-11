using System.Net;
using GoodStuff_Blazor.Models;
using GoodStuff_Blazor.Services.Interfaces;
using Microsoft.Identity.Client;

namespace GoodStuff_Blazor.Api;

public class GoodStuffApiClient(HttpClient client, IConfiguration configuration, IRequestMessageBuilder requestMessageBuilder)
{
    public async Task<ApiResult> SignUpAsync(SignUpModel model)
    {
        var apiResult = new ApiResult();

        try
        {
            var token = await GetAccessToken();
            var request = requestMessageBuilder.BuildPost(token, "user/signup", model);
            var response = await client.SendAsync(request);

            apiResult.Success = response.IsSuccessStatusCode;
            apiResult.StatusCode = response.StatusCode;

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                apiResult.ErrorMessage = response.StatusCode switch
                {
                    HttpStatusCode.BadRequest => "User already exist",
                    HttpStatusCode.InternalServerError => "Internal server error",
                    _ => !string.IsNullOrWhiteSpace(errorContent) ? errorContent : null
                };
            }
        }
        catch (Exception e)
        {
            // in the future, this will be replaced with a logger
            Console.WriteLine($"Couldn't sign up user {model.Email}. Error: {e.Message}");
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
            var token = await GetAccessToken();
            var request = requestMessageBuilder.BuildGet(token, $"user/signin?email={email}&password={password}");
            var response = await client.SendAsync(request);

            apiResult.Success = response.IsSuccessStatusCode;
            apiResult.StatusCode = response.StatusCode;

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                apiResult.ErrorMessage = response.StatusCode switch
                {
                    HttpStatusCode.Unauthorized => "Invalid email or password",
                    HttpStatusCode.InternalServerError => "Internal server error",
                    _ => !string.IsNullOrWhiteSpace(errorContent) ? errorContent : null
                };
            }
        }
        catch (Exception e)
        {
            // in the future, this will be replaced with a logger
            Console.WriteLine($"Couldn't SingIn user {email}. Error: {e.Message}");
            apiResult.Success = false;
            apiResult.ErrorMessage = "An error unexpected occurred while signing in";
            apiResult.StatusCode = HttpStatusCode.InternalServerError;
        }

        return apiResult;
    }

    public async Task<ApiResult> GetGpuProducts()
    {
        var apiResult = new ApiResult();
        try
        {
            var token = await GetAccessToken();
            var request = requestMessageBuilder.BuildGet(token, $"product/getallproductsbytype?type=GPU");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                apiResult.Content = await response.Content.ReadFromJsonAsync<List<GpuModel>>();
                apiResult.Success = true;
            }
            else
            {
                apiResult.ErrorMessage = await response.Content.ReadAsStringAsync();
                apiResult.Success = false;   
            }
        }
        catch (Exception e)
        {

            Console.WriteLine($"Couldn't GetGpuProducts Error: {e.Message}");
            apiResult.Success = false;
            apiResult.ErrorMessage = "An error unexpected occurred while retrieving GPU products.";
            apiResult.StatusCode = HttpStatusCode.InternalServerError;
        }

        return apiResult;
    }

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