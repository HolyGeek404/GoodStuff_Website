using Autofac.Features.Indexed;
using GoodStuff_DomainModels.Models.Enums;
using GoodStuff.Core.Services.Interfaces;

namespace GoodStuff.Core.Factories;

public class ProductApiClientFactory(IIndex<ProductCategories, IProductApiClient> productApiClients)
    : IProductApiClientFactory
{
    public IProductApiClient Get(ProductCategories type)
    {
        return productApiClients.TryGetValue(type, out var client) ? client : null;
    }
}