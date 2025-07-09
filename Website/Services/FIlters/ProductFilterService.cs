using Website.Factories.Interfaces;
using Website.Services.Interfaces;

namespace Website.Services.FIlters;


public class ProductFilterService(IFilterServiceFactory filterServiceFactory) : IProductFilterService
{
    public Dictionary<string, List<string>> GetFilters(List<Dictionary<string, string>> model, string category)
    {
        return filterServiceFactory.Get(category)!.GetFilters(model, category);
    }

    public List<Dictionary<string, string>> Filter(List<Dictionary<string, string>> model, string selectedFilters, string category)
    {
        return filterServiceFactory.Get(category)!.Filter(model, selectedFilters, category);
    }
}