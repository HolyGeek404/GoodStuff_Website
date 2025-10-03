using GoodStuff_DomainModels.Models.Enums;
using GoodStuff_DomainModels.Models.Products;
using GoodStuff.Website.Application.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GoodStuff.Website.Application.Services.Other;

public class CacheManager(IMemoryCache cache) : ICacheManager
{
    public IEnumerable<TProduct>? CheckProductsInCache<TProduct>(ProductCategories category)
        where TProduct : BaseProductModel
    {
        return cache.TryGetValue($"{Enum.GetName(category).ToUpper()}Products", out IEnumerable<TProduct> products)
            ? products
            : null;
    }

    public void AddProductToCache<TProduct>(IEnumerable<TProduct> product, ProductCategories category)
        where TProduct : BaseProductModel
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(1),
            Priority = CacheItemPriority.Normal
        };
        cache.Set<IEnumerable<TProduct>>($"{Enum.GetName(category).ToUpper()}Products", product.ToList().AsReadOnly(),
            cacheOptions);
    }
}