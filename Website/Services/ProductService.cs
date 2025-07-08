using Microsoft.Extensions.Caching.Memory;
using Website.Api;
using Website.Factories.Interfaces;

namespace Website.Services;

public class ProductService(ProductApiClient productApiClient,
                            IMemoryCache cache,
                            IFilterServiceFactory filterServiceFactory)
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
                cache.Set("GpuProducts", result.Content, cacheOptions);
                return (List<Dictionary<string, string>>)result.Content!;
            }
        }
        else
        {
            return products!;
        }

        return [];
    }

    public Dictionary<string, List<string>> GetFilters(List<Dictionary<string, string>> model, string category)
    {
       return filterServiceFactory.Get(category)!.CreateFilters(model,category);
    }

    public List<Dictionary<string, string>> Filter(string selectedFilters, string category)
    {
        return filterServiceFactory.Get(category)!.Filter(selectedFilters);
    }
}