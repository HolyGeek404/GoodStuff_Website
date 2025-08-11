using GoodStuff_DomainModels.Models.Products;
using Microsoft.Extensions.Caching.Memory;
using Website.Api;
using Website.Services.Factories;

namespace Website.Services.Product;

public interface IGpuProductService
{
    Task<List<Gpu>> GetModel(string category);
}

public class GpuProductService(
    IProductApiClientFactory  productApiClientFactory,
    IMemoryCache cache)
    : ProductService<Gpu>(cache), IGpuProductService
{
    private BaseProductApiClient ApiClient { get; set; }
    private readonly IMemoryCache _cache = cache;

    public override async Task<List<Gpu>> GetModel(string category)
    {
        if (_cache.TryGetValue("GpuProducts", out List<Gpu> products)) 
            return products ?? [];
        ApiClient = productApiClientFactory.Get(category);
        var result = await ApiClient.GetAllProductsByType(category);
        if (!result.Success) 
            return [];
        
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(1),
            Priority = CacheItemPriority.Normal
        };
        _cache.Set("GpuProducts", result.Content, cacheOptions);
        return (List<Gpu>)result.Content!;
    }
}