using Website.Factories.Interfaces;
using Website.Services.Interfaces;

namespace Website.Services.FIlters;


public class ProductFilterService(IFilterServiceFactory filterServiceFactory, ILogger<ProductFilterService> logger) : IProductFilterService
{
    public Dictionary<string, List<string>> GetFilters(List<Dictionary<string, string>> model, string category)
    {
        try
        {
            return filterServiceFactory.Get(category)!.GetFilters(model, category);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Couldn't get filters because: {ex}");
            throw;
        }
    }

    public List<Dictionary<string, string>> Filter(List<Dictionary<string, string>> model, Dictionary<string, List<string>> selectedFilters, string category)
    {
        try
        {
            return filterServiceFactory.Get(category)!.Filter(model, selectedFilters, category);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Couldn't filter products because: {ex}");
            throw;
        }
    }
}