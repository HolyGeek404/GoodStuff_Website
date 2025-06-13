using System.Net;
using Website.Models;
using Website.Services.Interfaces;

namespace Website.Api;

public class GoodStuffProductApiClient(HttpClient client, IConfigurationManager configuration, IRequestMessageBuilder requestMessageBuilder)
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
            }
        }
        catch (Exception e)
        {

            Console.WriteLine($"Couldn't GetGpuProducts Error: {e.Message}");
            apiResult.Success = false;
            apiResult.ErrorMessage = "An error unexpected occurred while retrieving GPU products.";
            apiResult.StatusCode = HttpStatusCode.InternalServerError;
        }

        return apiResult;
    }
}