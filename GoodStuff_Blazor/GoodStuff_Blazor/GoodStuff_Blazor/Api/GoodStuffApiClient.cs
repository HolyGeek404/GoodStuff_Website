using System.Net.Http.Headers;
using GoodStuff_Blazor.Models;
using Microsoft.Identity.Client;

namespace GoodStuff_Blazor.Api;

public class GoodStuffApiClient(HttpClient client)
{
    public async Task<bool> SignInAsync(SignInModel model)
    {
        var token = GetAccessToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.PostAsJsonAsync("user/signin", model);
        return response.IsSuccessStatusCode;
    }
    public async Task<bool> SignUpAsync(SignUpModel model)
    {
        var token = GetAccessToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("user/debug");
        return response.IsSuccessStatusCode;
    }

    private string GetAccessToken()
    {
        var tenantId = "e7b4669a-95b7-468d-8c55-4ba35609e82b";
        var clientId = "bcc2816c-5f68-4abf-9d01-6e0406d1011d";
        var clientSecret = "";
        var authority = $"https://login.microsoftonline.com/{tenantId}";
        var scope = "api://cd00bb0b-56b1-49ea-918b-18720712652a/.default"; // Or the resource you want to access

        var app = ConfidentialClientApplicationBuilder.Create(clientId)
            .WithClientSecret(clientSecret)
            .WithAuthority(new Uri(authority))
            .Build();


        var result = app.AcquireTokenForClient(new[] { scope }).ExecuteAsync().Result;

        string jwtToken = result.AccessToken;

        return jwtToken;
    }
}