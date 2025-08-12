using GoodStuff_DomainModels.Models.Products;
using Microsoft.Extensions.Caching.Memory;
using Website.Services.Interfaces;

namespace Website.Services.Product;

public class CpuProductService(
    IProductApiClientFactory productApiClientFactory,
    IMemoryCache cache)
    : BaseProductService(cache,productApiClientFactory), IProductService
{
    public async Task<object> GetModel(string category)
    {
        var products = await GetProducts<Cpu>(category);
        return products;
    }
}