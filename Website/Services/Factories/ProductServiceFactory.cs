using Autofac.Features.Indexed;
using Website.Services.Interfaces;

namespace Website.Services.Factories;

public class ProductServiceFactory(IIndex<string, IProductService> productServiceCollection) : IProductServiceFactory
{
    public IProductService Get(string type)
    {
        return productServiceCollection.TryGetValue(type, out var service) ? service : null;
    }
}