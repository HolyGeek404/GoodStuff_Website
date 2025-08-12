using Autofac.Features.Indexed;
using Website.Api;
using Website.Services.Interfaces;

namespace Website.Services.Factories;

public class ProductApiClientFactory(IIndex<string, BaseProductApiClient> productApiClients) : IProductApiClientFactory
{
    public BaseProductApiClient Get(string type)
    {
        return productApiClients.TryGetValue(type, out var client) ? client : null;
    }
}