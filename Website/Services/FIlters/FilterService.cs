using Website.Services.Interfaces;

namespace Website.Services.FIlters;

public class FilterService : IFilterService
{
    public Dictionary<string, List<string>> GetFilters(List<Dictionary<string, string>> model, string category)
    {
        var filters = FilterCategories.Get(category);
        var result = new Dictionary<string, List<string>>();

        foreach (var filter in filters)
        {
            var values = new HashSet<string>();

            foreach (var product in model)
                if (product.TryGetValue(filter, out var value) && !string.IsNullOrEmpty(value))
                    values.Add(value);

            result[filter] = [.. values.OrderBy(v => v)];
        }

        return result;
    }

    public List<Dictionary<string, string>> Filter(List<Dictionary<string, string>> model,
        Dictionary<string, List<string>> selectedFilters, string category)
    {
        if (selectedFilters.Count == 0)
            return model;

        var availableFilters = FilterCategories.Get(category);

        foreach (var filter in availableFilters)
            if (selectedFilters.TryGetValue(filter, out var filterValues))
                model = model.Where(x => filterValues.Contains(x[filter])).ToList();

        return model;
    }
}