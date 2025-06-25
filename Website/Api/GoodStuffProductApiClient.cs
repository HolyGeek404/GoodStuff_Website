using Website.Models;
using Website.Models.Products;
using Website.Services.Interfaces;

namespace Website.Api;

public class GoodStuffProductApiClient(HttpClient client, IConfiguration configuration, IRequestMessageBuilder requestMessageBuilder, ILogger<GoodStuffProductApiClient> logger)
{
    private readonly string _scope = configuration.GetSection("GoodStuffProductApi")["Scope"]!;

    public async Task<ApiResult> GetGpuProducts()
    {
        return await GetProduct<GpuModel>("GPU");
    }

    public async Task<ApiResult> GetCpuProducts()
    {
        return await GetProduct<CpuModel>("CPU");
    }

    public async Task<ApiResult> GetCoolerProducts()
    {
        return await GetProduct<CpuModel>("COOLER");
    }

    private async Task<ApiResult> GetProduct<TProduct>(string type)
    {
        var apiResult = new ApiResult();
        try
        {
            var request = await requestMessageBuilder.BuildGet(_scope, $"Product/GetAllProductsByType?type={type}");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                apiResult.Content = await response.Content.ReadFromJsonAsync<List<TProduct>>();
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
}