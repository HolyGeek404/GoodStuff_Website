using Website.Models;

namespace Website.Services.Interfaces;

public interface IProductApiClient
{
    Task<ApiResult> GetAllProductsByType(string type);
    Task<ApiResult> GetSingleProductById(string type, string id);
}