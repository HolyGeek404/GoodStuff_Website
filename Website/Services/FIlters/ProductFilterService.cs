using Website.Services.Interfaces;

namespace Website.Services.Filters;


public class ProductFilterService(IFilterService filterService, ILogger<ProductFilterService> logger) : IProductFilterService
{
    public Dictionary<string, List<string>> GetFilters(List<Dictionary<string, string>> model, string category)
    {
        try
        {
            return filterService.GetFilters(model, category);
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
            return filterService.Filter(model, selectedFilters, category);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Couldn't filter products because: {ex}");
            throw;
        }
    }
}