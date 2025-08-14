using Autofac.Features.Indexed;
using Website.Api;
using Website.Services.Interfaces;

namespace Website.Factories;

public class ProductApiClientFactory(IIndex<string, IProductApiClient> productApiClients) : IProductApiClientFactory
{
    public IProductApiClient Get(string type)
    {
        return productApiClients.TryGetValue(type, out var client) ? client : null;
    }
}