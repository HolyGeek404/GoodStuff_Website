using Website.Models;
using Website.Services.Interfaces;

namespace Website.Api;

public class ProductApiClient<TProduct>(
    IHttpClientFactory clientFactory,
    IConfiguration configuration,
    IRequestMessageBuilder requestMessageBuilder,
    ILogger<ProductApiClient<TProduct>> logger) : IProductApiClient
{
    private readonly string _scope = configuration.GetSection("GoodStuffProductApi")["Scope"]!;

    private async Task<ApiResult> Send<T>(HttpRequestMessage request)
    {
        var apiResult = new ApiResult();
        try
        {
            var client = clientFactory.CreateClient("ProductClient");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                apiResult.Content = await response.Content.ReadFromJsonAsync<T>();
                apiResult.Success = true;
            }
            else
            {
                apiResult.ErrorMessage = await response.Content.ReadAsStringAsync();
                apiResult.Success = false;
                logger.LogError("Couldn't get products. Http Code: {ResponseStatusCode}. Error Message: {ApiResultErrorMessage}", response.StatusCode, apiResult.ErrorMessage);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Couldn't GetGpuProducts Error: {EMessage}", e.Message);
            throw;
        }
        
        return apiResult;
    }

    public async Task<ApiResult> GetAllProductsByType(string type)
    {
        var request = await requestMessageBuilder.BuildGet(_scope, $"Product/GetAllProductsByType?type={type}");
        var response = await Send<IEnumerable<TProduct>>(request);
        return response;
    }

    public async Task<ApiResult> GetSingleProductById(string type, string id)
    {
        var request = await requestMessageBuilder.BuildGet(_scope, $"Product/GetProductById?type={type}&id={id}");
        var response = await Send<TProduct>(request);
        return response;
    }
}