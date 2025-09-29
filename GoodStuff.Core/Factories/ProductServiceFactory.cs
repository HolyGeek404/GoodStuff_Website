using Autofac.Features.Indexed;
using GoodStuff_DomainModels.Models.Enums;
using GoodStuff.Core.Services.Interfaces;

namespace GoodStuff.Core.Factories;

public class ProductServiceFactory(IIndex<ProductCategories, IProductService> productServiceCollection)
    : IProductServiceFactory
{
    public IProductService Get(ProductCategories type)
    {
        return productServiceCollection.TryGetValue(type, out var service) ? service : null;
    }
}