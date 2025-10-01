using GoodStuff_DomainModels.Models.Enums;
using GoodStuff_DomainModels.Models.Products;
using GoodStuff.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoodStuff.Core.Services.Product;

public class ProductService<TProduct>(
    ProductCategories category,
    ICacheManager cache,
    IProductApiClientFactory productApiClientFactory,
    IProductFilterServiceFactory productFilterServiceFactory,
    ILogger<ProductService<TProduct>> logger)
    : IProductService where TProduct : BaseProductModel
{
    #region Product

    public async Task<IEnumerable<BaseProductModel>> GetProducts()
    {
        try
        {
            logger.LogInformation("Fetching products for category {Category}", category);

            var products = CheckProductsInCache();
            if (products != null && products.Any())
            {
                logger.LogInformation("Products found in cache for category {Category}", category);
                return products;
            }

            logger.LogInformation("No products in cache. Fetching from API for category {Category}", category);
            products = await GetAllProductsByType();

            var baseProducts = products as TProduct[] ?? products.ToArray();
            AddProductToCache(baseProducts);

            logger.LogInformation("Products successfully fetched and cached for category {Category}", category);
            return baseProducts;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching products for category {Category}", category);
            return Enumerable.Empty<BaseProductModel>();
        }
    }

    private async Task<TProduct?> GetProduct(string id)
    {
        try
        {
            logger.LogInformation("Fetching product with ID {ProductId} for category {Category}", id, category);

            var productsInCache = CheckProductsInCache();
            var searchedProduct = productsInCache.FirstOrDefault(x => x.ProductId == id);

            if (searchedProduct != null)
            {
                logger.LogInformation("Product {ProductId} found in cache", id);
                return searchedProduct;
            }

            logger.LogInformation("Product {ProductId} not found in cache. Fetching from API", id);
            searchedProduct = await GetProductById(id);

            return searchedProduct;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching product {ProductId} for category {Category}", id,
                category);
            return null;
        }
    }

    #endregion

    #region Filter

    public IEnumerable<BaseProductModel> FilterProducts(IEnumerable<BaseProductModel> products,
        Dictionary<string, List<string>> selectedFilters)
    {
        try
        {
            logger.LogInformation("Filtering products for category {Category}", category);
            var filterService = productFilterServiceFactory.Get(category);
            var filteredProducts = filterService.Filter(products, selectedFilters);
            logger.LogInformation("Filtering completed for category {Category}", category);
            return filteredProducts;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while filtering products for category {Category}", category);
            return Enumerable.Empty<BaseProductModel>();
        }
    }

    public Dictionary<string, List<string>> GetFilters(IEnumerable<BaseProductModel> productList)
    {
        try
        {
            logger.LogInformation("Getting filters for category {Category}", category);
            var filterService = productFilterServiceFactory.Get(category);
            var filters = filterService.GetFilters(productList);
            logger.LogInformation("Filters successfully retrieved for category {Category}", category);
            return filters;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while retrieving filters for category {Category}", category);
            return new Dictionary<string, List<string>>();
        }
    }

    #endregion

    #region Cache

    private IEnumerable<TProduct> CheckProductsInCache()
    {
        try
        {
            logger.LogDebug("Checking cache for products in category {Category}", category);
            return cache.CheckProductsInCache<TProduct>(category);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while checking cache for category {Category}", category);
            return Enumerable.Empty<TProduct>();
        }
    }

    private void AddProductToCache(IEnumerable<TProduct> product)
    {
        try
        {
            logger.LogDebug("Adding products to cache for category {Category}", category);
            cache.AddProductToCache(product, category);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while adding products to cache for category {Category}", category);
        }
    }

    #endregion

    #region Api Calls

    private async Task<IEnumerable<TProduct>> GetAllProductsByType()
    {
        try
        {
            logger.LogInformation("Calling API to fetch all products for category {Category}", category);
            var client = productApiClientFactory.Get(category);
            var response = await client.GetAllProductsByType(category);
            logger.LogInformation("API call successful for category {Category}", category);
            return (IEnumerable<TProduct>)response.Content;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching all products from API for category {Category}",
                category);
            return Enumerable.Empty<TProduct>();
        }
    }

    private async Task<TProduct?> GetProductById(string id)
    {
        try
        {
            logger.LogInformation("Calling API to fetch product {ProductId} for category {Category}", id, category);
            var client = productApiClientFactory.Get(category);
            var response = await client.GetSingleProductById(category, id);
            logger.LogInformation("API call successful for product {ProductId} in category {Category}", id, category);
            return (TProduct)response.Content;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching product {ProductId} from API for category {Category}",
                id, category);
            return null;
        }
    }

    #endregion
}