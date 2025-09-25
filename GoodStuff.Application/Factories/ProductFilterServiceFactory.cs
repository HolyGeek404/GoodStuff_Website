using Autofac.Features.Indexed;
using GoodStuff_DomainModels.Models.Enums;
using GoodStuff.Application.Services.Interfaces;

namespace GoodStuff.Application.Factories;

public class ProductFilterServiceFactory(
    IIndex<ProductCategories, IProductFilterService> productFilterServiceCollection) : IProductFilterServiceFactory
{
    public IProductFilterService Get(ProductCategories type)
    {
        return productFilterServiceCollection.TryGetValue(type, out var client) ? client : null;
    }
}