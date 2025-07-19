using Microsoft.Extensions.Caching.Memory;
using Website.Api;
using Website.Services.Interfaces;

namespace Website.Services;

public class ProductService(ProductApiClient productApiClient,
                            IMemoryCache cache) : IProductService
{
    public async Task<List<Dictionary<string, string>>> GetModel(string category)
    {
        if (!cache.TryGetValue($"{category}Products", out List<Dictionary<string, string>>? products))
        {
            var result = await productApiClient.GetAllProductsByType(category);
            if (!result.Success) return [];
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(1),
                Priority = CacheItemPriority.Normal
            };
                
            cache.Set($"{category}GpuProducts", result.Content, cacheOptions);
            return (List<Dictionary<string, string>>)result.Content!;
        }
        else
        {
            if (products != null) return products;
        }

        return [];
    }
}