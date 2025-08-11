using GoodStuff_DomainModels.Models.Products;
using Website.Models;
using Website.Services.Interfaces;

namespace Website.Api;

public class CpuProductApiClient(
    HttpClient client,
    IConfiguration configuration,
    IRequestMessageBuilder requestMessageBuilder,
    ILogger<BaseProductApiClient> logger) :  BaseProductApiClient(client, configuration, logger), IProductApiClient
{
    public override async Task<ApiResult> GetAllProductsByType(string type)
    {
        var request = await requestMessageBuilder.BuildGet(Scope, $"Product/GetAllProductsByType?type={type}");
        var response = await Send<List<Cpu>>(request);
        return response;
    }

    public override async Task<ApiResult> GetSingleProductById(string type, string id)
    {
        var request = await requestMessageBuilder.BuildGet(Scope, $"Product/GetProductById?type={type}&id={id}");
        var response = await Send<Cpu>(request);
        return response;
    }
}