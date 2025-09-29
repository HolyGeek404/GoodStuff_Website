namespace GoodStuff.Application.Services.Interfaces;

public interface ITokenProvider
{
    Task<string> GetAccessToken(string scope);
}