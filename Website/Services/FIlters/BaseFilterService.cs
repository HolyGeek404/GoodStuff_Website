namespace Website.Services.Filters;

public class BaseFilterService
{
    public Dictionary<string, List<string>> GetFilters(List<Dictionary<string, string>> model, string category)
    {
        var filters = FilterCategories.Get(category);
        var result = new Dictionary<string, List<string>>();

        foreach (var filter in filters)
        {
            var values = new HashSet<string>();

            foreach (var product in model)
            {
                if (product.TryGetValue(filter, out var value) && !string.IsNullOrEmpty(value))
                {
                    values.Add(value);
                }
            }

            result[filter] = [.. values.OrderBy(v => v)];
        }

        return result;
    }
}