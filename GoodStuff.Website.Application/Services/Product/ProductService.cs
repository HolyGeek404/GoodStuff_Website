using GoodStuff_DomainModels.Models.Enums;
using GoodStuff_DomainModels.Models.Products;
using GoodStuff.Website.Application.Services.Interfaces;

namespace GoodStuff.Website.Application.Services.Product;

public class ProductService<TProduct>(
    ProductCategories category,
    ICacheManager cache,
    IProductApiClientFactory productApiClientFactory,
    IProductFilterServiceFactory productFilterServiceFactory)
    : IProductService where TProduct : BaseProductModel
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
        if (searchedProduct != null) return searchedProduct;

        searchedProduct = await GetProductById(id);
        return searchedProduct;
    }

    #endregion

    #region Filter

    public IEnumerable<BaseProductModel> FilterProducts(IEnumerable<BaseProductModel> products,
        Dictionary<string, List<string>> selectedFilters)
    {
        var filterService = productFilterServiceFactory.Get(category);
        var filteredProducts = filterService.Filter(products, selectedFilters);
        return filteredProducts;
    }

    public Dictionary<string, List<string>> GetFilters(IEnumerable<BaseProductModel> productList)
    {
        var filterService = productFilterServiceFactory.Get(category);
        var filters = filterService.GetFilters(productList);
        return filters;
    }

    #endregion

    #region Cache

    private IEnumerable<TProduct> CheckProductsInCache()
    {
        return cache.CheckProductsInCache<TProduct>(category);
    }

    private void AddProductToCache(IEnumerable<TProduct> product)
    {
        cache.AddProductToCache(product, category);
    }

    #endregion

    #region Api Calls

    private async Task<IEnumerable<TProduct>> GetAllProductsByType()
    {
        var client = productApiClientFactory.Get(category);
        var response = await client.GetAllProductsByType(category);
        return (IEnumerable<TProduct>)response.Content;
    }

    private async Task<TProduct> GetProductById(string id)
    {
        var client = productApiClientFactory.Get(category);
        var response = await client.GetSingleProductById(category, id);
        return (TProduct)response.Content;
    }

    #endregion
}