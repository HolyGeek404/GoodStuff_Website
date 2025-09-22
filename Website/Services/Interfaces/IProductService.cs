using GoodStuff_DomainModels.Models.Products;

namespace Website.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<BaseProductModel>> GetProducts();

    IEnumerable<BaseProductModel> FilterProducts(IEnumerable<BaseProductModel> products,
        Dictionary<string, List<string>> selectedFilters);

    Dictionary<string, List<string>> GetFilters(IEnumerable<BaseProductModel> productList);
}