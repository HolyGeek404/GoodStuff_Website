using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using GoodStuff_Blazor.Services.Interfaces;

namespace GoodStuff_Blazor.Services;

public class RequestMessageBuilder : IRequestMessageBuilder
{
    public HttpRequestMessage BuildPost(string token, string endpoint, object body)
    {
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

    public HttpRequestMessage BuildGet(string token, string endpoint)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return request;
    }
}