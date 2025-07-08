using Website.Models;
using Website.Services.Interfaces;

namespace Website.Api;

public class ProductApiClient(HttpClient client, IConfiguration configuration, IRequestMessageBuilder requestMessageBuilder, ILogger<ProductApiClient> logger)
{
    private readonly string _scope = configuration.GetSection("GoodStuffProductApi")["Scope"]!;

    public async Task<ApiResult> GetAllProductsByType(string type)
    {
        var apiResult = new ApiResult();
        try
        {
            var request = await requestMessageBuilder.BuildGet(_scope, $"Product/GetAllProductsByType?type={type}");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                apiResult.Content = await response.Content.ReadFromJsonAsync<List<Dictionary<string, string>>>();
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

    public async Task<ApiResult> GetSingleProductById(string type, string id)
    {
        var apiResult = new ApiResult();
        try
        {
            var request = await requestMessageBuilder.BuildGet(_scope, $"Product/GetProductById?type={type}&id={id}");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                apiResult.Content = await response.Content.ReadFromJsonAsync<List<Dictionary<string, string>>>();
                apiResult.Success = true;
            }
            else
            {
                apiResult.ErrorMessage = await response.Content.ReadAsStringAsync();
                apiResult.Success = false;
                logger.LogError($"Couldn't get product. Http Code: {response.StatusCode}. Error Message: {apiResult.ErrorMessage}");
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Couldn't GetProductById Error: {e.Message}");
            throw;
        }

        return apiResult;
    }
}