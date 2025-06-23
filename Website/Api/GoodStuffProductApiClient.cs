using System.Net;
using Website.Models;
using Website.Services.Interfaces;

namespace Website.Api;

public class GoodStuffProductApiClient(HttpClient client, IConfiguration configuration, IRequestMessageBuilder requestMessageBuilder, ILogger<GoodStuffProductApiClient> logger)
{
    private readonly string _scope = configuration.GetSection("GoodStuffProductApi")["Scope"]!;

    public async Task<ApiResult> GetGpuProducts()
    {
        var apiResult = new ApiResult();
        try
        {
            var request = await requestMessageBuilder.BuildGet(_scope, $"Product/GetAllProductsByType?type=GPU");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                apiResult.Content = await response.Content.ReadFromJsonAsync<List<GpuModel>>();
                apiResult.Success = true;
            }
            else
            {
                apiResult.ErrorMessage = await response.Content.ReadAsStringAsync();
                apiResult.Success = false;
                logger.LogError($"Couldn't GetGpuProducts. Http Code: {response.StatusCode}. Error Message: {apiResult.ErrorMessage}");
            }
        }
        catch (Exception e)
        {

            logger.LogError(e,$"Couldn't GetGpuProducts Error: {e.Message}");
            apiResult.Success = false;
            apiResult.ErrorMessage = "An error unexpected occurred while retrieving GPU products.";
            apiResult.StatusCode = HttpStatusCode.InternalServerError;
        }

        return apiResult;
    }
}