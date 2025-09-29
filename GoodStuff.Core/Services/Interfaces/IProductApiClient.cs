using GoodStuff_DomainModels.Models.Enums;
using GoodStuff.Domain.Models;

namespace GoodStuff.Core.Services.Interfaces;

public interface IProductApiClient
{
    Task<ApiResult> GetAllProductsByType(ProductCategories type);
    Task<ApiResult> GetSingleProductById(ProductCategories type, string id);
}