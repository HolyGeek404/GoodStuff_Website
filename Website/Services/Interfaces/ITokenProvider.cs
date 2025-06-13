namespace Website.Services.Interfaces;

public interface ITokenProvider
{
    Task<string> GetAccessToken(string scope);
}