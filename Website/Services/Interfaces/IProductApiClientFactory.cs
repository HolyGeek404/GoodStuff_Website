using Website.Api;

namespace Website.Services.Interfaces;

public interface IProductApiClientFactory
{
    IProductApiClient Get(string type);
}