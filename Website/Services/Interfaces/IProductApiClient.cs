using GoodStuff_DomainModels.Models.Enums;
using Website.Models;

namespace Website.Services.Interfaces;

public interface IProductApiClient
{
    Task<ApiResult> GetAllProductsByType(ProductCategories type);
    Task<ApiResult> GetSingleProductById(ProductCategories type, string id);
}