namespace GoodStuff.Website.Application.Services.Interfaces;

public interface IRequestMessageBuilder
{
    Task<HttpRequestMessage> BuildPost(string scope, string endpoint, object body);
    Task<HttpRequestMessage> BuildGet(string scope, string endpoint);
}