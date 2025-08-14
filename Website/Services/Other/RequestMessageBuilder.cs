using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Website.Services.Interfaces;

namespace Website.Services.Other;

public class RequestMessageBuilder(ITokenProvider tokenProvider) : IRequestMessageBuilder
{
    public async Task<HttpRequestMessage> BuildPost(string scope, string endpoint, object body)
    {
        var token = await tokenProvider.GetAccessToken(scope);
        var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
        {
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"),
            Headers =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", token)
            }
        };

        return request;
    }

    public async Task<HttpRequestMessage> BuildGet(string scope, string endpoint)
    {
        var token = await tokenProvider.GetAccessToken(scope);
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return request;
    }
}