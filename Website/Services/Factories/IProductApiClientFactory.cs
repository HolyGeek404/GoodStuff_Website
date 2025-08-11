using Website.Api;
using Website.Services.Interfaces;

namespace Website.Services.Factories;

public interface IProductApiClientFactory
{
    BaseProductApiClient Get(string type);
}