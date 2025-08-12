using Microsoft.Extensions.Caching.Memory;
using Website.Services.Interfaces;

namespace Website.Services.Product;

public abstract class BaseProductService(
    IMemoryCache cache,
    IProductApiClientFactory productApiClientFactory)
{
    protected async Task<List<T>> GetProducts<T>(string category)
    {
        var products = CheckProductsInCache<T>(category);
        if (products != null) 
            return products;
        
        products = await GetAllProductsByType<T>(category);
        AddProductToCache(category, products);
        
        return products;
    }

    private List<T> CheckProductsInCache<T>(string category)
    {
        return cache.TryGetValue($"{category}Products", out List<T> products) ? products : null;
    }
    private void AddProductToCache<T>(string category, List<T> product)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(1),
            Priority = CacheItemPriority.Normal
        };
        cache.Set($"{category}Products", product, cacheOptions);
    }
    private async Task<List<T>> GetAllProductsByType<T>(string category)
    {
        var client = productApiClientFactory.Get(category.ToUpper());
        var response = await client.GetAllProductsByType(category);
        return (List<T>)response.Content;
    }
}