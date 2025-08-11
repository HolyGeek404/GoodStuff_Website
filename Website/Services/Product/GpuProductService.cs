using GoodStuff_DomainModels.Models.Products;
using Microsoft.Extensions.Caching.Memory;
using Website.Api;

namespace Website.Services.Product;

public class GpuProductService(BaseProductApiClient baseProductApiClient, IMemoryCache cache)
    : ProductService<Gpu>(baseProductApiClient, cache)
{
    private readonly BaseProductApiClient _baseProductApiClient = baseProductApiClient;
    private readonly IMemoryCache _cache = cache;

    public override async Task<List<Gpu>> GetModel(string category)
    {
        if (_cache.TryGetValue($"{category}Products", out List<Gpu> products)) 
            return products ?? [];
        
        var result = await _baseProductApiClient.GetAllProductsByType(category);
        if (!result.Success) 
            return [];
        
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(1),
            Priority = CacheItemPriority.Normal
        };
        _cache.Set($"{category}GpuProducts", result.Content, cacheOptions);
        return (List<Gpu>)result.Content!;
    }
}