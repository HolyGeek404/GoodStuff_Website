using Autofac.Features.Indexed;
using GoodStuff_DomainModels.Models.Enums;
using GoodStuff.Core.Services.Interfaces;

namespace GoodStuff.Core.Factories;

public class ProductFilterServiceFactory(
    IIndex<ProductCategories, IProductFilterService> productFilterServiceCollection) : IProductFilterServiceFactory
{
    public IProductFilterService Get(ProductCategories type)
    {
        return productFilterServiceCollection.TryGetValue(type, out var client) ? client : null;
    }
}