using GoodStuff_DomainModels.Models.Products;
using Microsoft.Extensions.Caching.Memory;
using Website.Services.Interfaces;

namespace Website.Services.Product;

public class ProductService<TProduct>(
    string category,
    IMemoryCache cache,
    IProductApiClientFactory productApiClientFactory)
    : IProductService
    where TProduct : BaseProductModel
{
    #region Product

    public async Task<IEnumerable<BaseProductModel>> GetProducts()
    {
        var products = CheckProductsInCache();
        if (products != null) return products;
        products = await GetAllProductsByType();
        var baseProducts = products as TProduct[] ?? products.ToArray();
        AddProductToCache(baseProducts);
        return baseProducts;
    }

    private async Task<TProduct> GetProduct(string id)
    {
        var productsInCache = CheckProductsInCache();
        var searchedProduct = productsInCache.FirstOrDefault(x => x.ProductId == id);
        if (searchedProduct != null)
        {
            return searchedProduct;
        }

        searchedProduct = await GetProductById(id);
        return searchedProduct;
    }

    #endregion

    #region Cache

    private IEnumerable<TProduct> CheckProductsInCache()
    {
        return cache.TryGetValue($"{category}Products", out IEnumerable<TProduct> products) ? products : null;
    }

    private void AddProductToCache(IEnumerable<TProduct> product)
    {
        var cacheOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(1),
            Priority = CacheItemPriority.Normal
        };
        cache.Set($"{category}Products", product.ToList().AsReadOnly(), cacheOptions);
    }

    #endregion

    #region Api Calls

    private async Task<IEnumerable<TProduct>> GetAllProductsByType()
    {
        var client = productApiClientFactory.Get(category.ToUpper());
        var response = await client.GetAllProductsByType(category);
        return (IEnumerable<TProduct>)response.Content;
    }

    private async Task<TProduct> GetProductById(string id)
    {
        var client = productApiClientFactory.Get(category.ToUpper());
        var response = await client.GetSingleProductById(category, id);
        return (TProduct)response.Content;
    }

    #endregion
}