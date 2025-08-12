using Website.Api;

namespace Website.Services.Interfaces;

public interface IProductApiClientFactory
{
    BaseProductApiClient Get(string type);
}