using Autofac.Features.Indexed;
using GoodStuff_DomainModels.Models.Enums;
using GoodStuff.Application.Services.Interfaces;

namespace GoodStuff.Application.Factories;

public class ProductServiceFactory(IIndex<ProductCategories, IProductService> productServiceCollection)
    : IProductServiceFactory
{
    public IProductService Get(ProductCategories type)
    {
        return productServiceCollection.TryGetValue(type, out var service) ? service : null;
    }
}