namespace GoodStuff.Core.Services.Interfaces;

public interface ITokenProvider
{
    Task<string> GetAccessToken(string scope);
}