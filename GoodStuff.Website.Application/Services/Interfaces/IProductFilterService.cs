using GoodStuff_DomainModels.Models.Products;

namespace GoodStuff.Website.Application.Services.Interfaces;

public interface IProductFilterService
{
    List<BaseProductModel> Filter(IEnumerable<BaseProductModel> productList,
        Dictionary<string, List<string>> selectedFilters);

    Dictionary<string, List<string>> GetFilters(IEnumerable<BaseProductModel> products);
}