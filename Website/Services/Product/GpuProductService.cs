// using GoodStuff_DomainModels.Models.Products;
// using Microsoft.AspNetCore.Components;
// using Microsoft.Extensions.Caching.Memory;
// using Website.Services.Interfaces;
//
// namespace Website.Services.Product;
//
// public class GpuProductService(
//     IProductApiClientFactory productApiClientFactory,
//     IViewBuilderFactory viewBuilderFactory,
//     IMemoryCache cache)
//     : ProductService<GpuModel>(cache,productApiClientFactory, viewBuilderFactory), IProductService
// {
//     public async Task<MarkupString> BuildPreview(string category)
//     {
//         var products = await BuildProductsPreview(category);
//         return products;
//     }
//     public async Task<object> GetProductById(string category, string id)
//     {
//         var product = await GetProduct(category, id);
//         return product;
//     }
// }