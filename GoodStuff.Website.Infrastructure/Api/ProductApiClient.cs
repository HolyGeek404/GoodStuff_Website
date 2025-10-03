using System.Net;
using System.Net.Http.Json;
using GoodStuff_DomainModels.Models.Enums;
using GoodStuff_DomainModels.Models.Products;
using GoodStuff.Website.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GoodStuff.Website.Infrastructure.Api;

public class ProductApiClient(
    IHttpClientFactory clientFactory,
    IConfiguration configuration,
    IRequestMessageBuilder requestMessageBuilder,
    ILogger<ProductApiClient> logger) : IProductApiClient
{
    private readonly string _scope = configuration.GetSection("GoodStuffProductApi")["Scope"]!;

    public async Task<IEnumerable<TProduct>> GetAllProductsByType<TProduct>(ProductCategories type)
        where TProduct : BaseProductModel
    {
        var request = await requestMessageBuilder.BuildGet(_scope, $"Product/GetAllProductsByType?type={type}");
        var response = await Send<IEnumerable<TProduct>>(request);
        return response;
    }

    public async Task<TProduct> GetSingleProductById<TProduct>(ProductCategories type, string id)
        where TProduct : BaseProductModel
    {
        var request = await requestMessageBuilder.BuildGet(_scope, $"Product/GetProductById?type={type}&id={id}");
        var response = await Send<TProduct>(request);
        return response;
    }

    private async Task<T> Send<T>(HttpRequestMessage request)
    {
        try
        {
            var client = clientFactory.CreateClient("ProductClient");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return default!;

                var content = await response.Content.ReadFromJsonAsync<T>();
                return content ?? throw new InvalidOperationException("Response deserialized to null");
            }

            var err = await response.Content.ReadAsStringAsync();
            logger.LogError(
                "Couldn't get products. Http Code: {ResponseStatusCode}. Error Message: {ApiResultErrorMessage}",
                response.StatusCode, err);

            throw new HttpRequestException($"Request failed: {response.StatusCode}: {err}");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Couldn't GetGpuProducts Error: {EMessage}", e.Message);
            throw;
        }
    }
}