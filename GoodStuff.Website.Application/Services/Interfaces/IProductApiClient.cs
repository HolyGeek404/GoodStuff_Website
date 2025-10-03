using GoodStuff_DomainModels.Models.Enums;
using GoodStuff_DomainModels.Models.Products;

namespace GoodStuff.Website.Application.Services.Interfaces;

public interface IProductApiClient
{
    Task<IEnumerable<TProduct>> GetAllProductsByType<TProduct>(ProductCategories type)
        where TProduct : BaseProductModel;

    Task<TProduct> GetSingleProductById<TProduct>(ProductCategories type, string id) where TProduct : BaseProductModel;
}