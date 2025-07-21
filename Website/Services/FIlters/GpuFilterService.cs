using Website.Services.Interfaces;

namespace Website.Services.Filters;

public class GpuFilterService : BaseFilterService, IProductFilterService
{
    public List<Dictionary<string, string>> Filter(List<Dictionary<string, string>> model, Dictionary<string, List<string>> selectedFilters, string category)
    {
        if (model == null || selectedFilters.Count == 0)
            return model ?? [];


        if (selectedFilters == null)
            return model;

        var avaiableFilters = FilterCategories.Get(category);

        foreach (var filter in avaiableFilters)
        {
            if (selectedFilters.TryGetValue(filter, out List<string> filterValues))
            {
                model = model.Where(x => filterValues.Contains(x[filter])).ToList();
            }
        }

        return model;
    }
}