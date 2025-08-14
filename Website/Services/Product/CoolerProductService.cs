// using GoodStuff_DomainModels.Models.Products;
// using Microsoft.Extensions.Caching.Memory;
// using Website.Services.Interfaces;
//
// namespace Website.Services.Product;
//
// public class CoolerProductService(
//     IProductApiClientFactory productApiClientFactory,
//     IMemoryCache cache)
//     : ProductService<CoolerModel>(cache,productApiClientFactory), IProductService
// {
//     public async Task<object> GetProductsByType(string category)
//     {
//         var products = await GetProducts(category);
//         return products;
//     }
//     public async Task<object> GetProductById(string category, string id)
//     {
//         var product = await GetProduct(category, id);
//         return product;
//     }
// }