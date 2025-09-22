using Autofac.Features.Indexed;
using Website.Services.Interfaces;

namespace Website.Factories;

public class ProductFilterServiceFactory(IIndex<string, IProductFilterService> productFilterServiceCollection)
    : IProductFilterServiceFactory
{
    public IProductFilterService Get(string type)
    {
        return productFilterServiceCollection.TryGetValue(type, out var client) ? client : null;
    }
}