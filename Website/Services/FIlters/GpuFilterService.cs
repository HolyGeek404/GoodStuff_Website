using Newtonsoft.Json;
using Website.Models.Filters;
using Website.Services.Interfaces;

namespace Website.Services.FIlters;

public class GpuFilterService : BaseFilterService, IFilterService
{
    public List<Dictionary<string, string>> Filter(string selectedFilters)
    {
        var result = new List<Dictionary<string, string>>();
        var filterModel = JsonConvert.DeserializeObject<GpuFilterModel>(selectedFilters);

        return result;
    }
}