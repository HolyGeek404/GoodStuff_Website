using Website.Models;
using Website.Services.Interfaces;
using Website.Services.Product;

namespace Website.Api;

public class BaseProductApiClient(HttpClient client, IConfiguration configuration, IRequestMessageBuilder requestMessageBuilder, ILogger<BaseProductApiClient> logger)
{
    private readonly string _scope = configuration.GetSection("GoodStuffProductApi")["Scope"]!;

    private async Task<ApiResult> Send(HttpRequestMessage request)
    {
        var apiResult = new ApiResult();
        try
        {
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                apiResult.Content = await response.Content.ReadFromJsonAsync<object>();
                apiResult.Success = true;
            }
            else
            {
                apiResult.ErrorMessage = await response.Content.ReadAsStringAsync();
                apiResult.Success = false;
                logger.LogError($"Couldn't get products. Http Code: {response.StatusCode}. Error Message: {apiResult.ErrorMessage}");
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Couldn't GetGpuProducts Error: {e.Message}");
            throw;
        }
        
        return apiResult;
    }
    
    public async Task<ApiResult> GetAllProductsByType(string type)
    {
        var request = await requestMessageBuilder.BuildGet(_scope, $"Product/GetAllProductsByType?type={type}");
        var response = await Send(request);
        // factory here
        return response;
    }

    public async Task<ApiResult> GetSingleProductById(string type, string id)
    {
        var request = await requestMessageBuilder.BuildGet(_scope, $"Product/GetProductById?type={type}&id={id}");
        var response = await Send(request);
        return response;
    }
}