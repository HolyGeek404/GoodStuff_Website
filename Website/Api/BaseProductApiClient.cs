using Website.Models;

namespace Website.Api;

public abstract class BaseProductApiClient(HttpClient client, IConfiguration configuration, ILogger<BaseProductApiClient> logger)
{
    protected readonly string Scope = configuration.GetSection("GoodStuffProductApi")["Scope"]!;

    protected async Task<ApiResult> Send<T>(HttpRequestMessage request)
    {
        var apiResult = new ApiResult();
        try
        {
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

    public abstract Task<ApiResult> GetAllProductsByType(string type);
    public abstract Task<ApiResult> GetSingleProductById(string type, string id);

}