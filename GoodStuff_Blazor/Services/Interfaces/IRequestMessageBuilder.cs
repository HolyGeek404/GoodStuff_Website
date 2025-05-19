namespace GoodStuff_Blazor.Services.Interfaces;

public interface IRequestMessageBuilder
{
    HttpRequestMessage BuildPost(string token, string endpoint, object body);
    HttpRequestMessage BuildGet(string token, string endpoint);
}