using System.Net.Http.Headers;
using GoodStuff_Blazor.Models;
using Microsoft.Identity.Client;

namespace GoodStuff_Blazor.Api;

public class GoodStuffApiClient(HttpClient client, IConfiguration configuration)
{
    public async Task<bool> SignInAsync(SignInModel model)
    {
        var token = await GetAccessToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.PostAsJsonAsync("user/signin", model);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> SignUpAsync(SignUpModel model)
    {
        var token = await GetAccessToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.PostAsJsonAsync("user/signup", model);
        return response.IsSuccessStatusCode;
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
    //private async Task<string> BuildRequest()
}