using Microsoft.Identity.Client;
using Website.Services.Interfaces;

namespace Website.Services;

public class TokenProvider(IConfigurationManager configuration) : ITokenProvider
{
    public async Task<string> GetAccessToken(string scope)
    {
        var azure = configuration.GetSection("AzureAd");
        var tenantId = azure["TenantId"];
        var clientId = azure["ClientId"];
        var clientSecret = azure["ClientSecret"];
        var authority = $"https://login.microsoftonline.com/{tenantId}";

        var app = ConfidentialClientApplicationBuilder.Create(clientId)
            .WithClientSecret(clientSecret)
            .WithAuthority(new Uri(authority))
            .Build();

        var result = await app.AcquireTokenForClient([scope]).ExecuteAsync();

        return result.AccessToken;
    }
}