using GoodStuff_DomainModels.Models.Enums;
using GoodStuff.Website.Domain.Models;

namespace GoodStuff.Website.Application.Services.Interfaces;

public interface IProductApiClient
{
    Task<ApiResult> GetAllProductsByType(ProductCategories type);
    Task<ApiResult> GetSingleProductById(ProductCategories type, string id);
}