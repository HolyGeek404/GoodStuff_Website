using Microsoft.Extensions.Caching.Memory;
using Website.Api;

namespace Website.Services;

public class ProductService(GoodStuffProductApiClient productApiClient, IMemoryCache cache)
{
    public async Task<List<Dictionary<string, string>>> GetModel(string category)
    {
        if (!cache.TryGetValue("GpuProducts", out List<Dictionary<string, string>>? products))
        {
            var result = await productApiClient.GetAllProductsByType(category);
            if (result.Success)
            {
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(1),
                    Priority = CacheItemPriority.Normal
                };
                cache.Set("GpuProducts",result.Content, cacheOptions);
                return (List<Dictionary<string, string>>)result.Content!;
                // Filters = filterService.CreateFilters(Model, Category);
            }
        }
        else
        {
            return products!;
        }

        return [];
    }
}