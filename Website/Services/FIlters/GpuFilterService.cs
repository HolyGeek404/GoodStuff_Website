using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Website.Models.Filters;
using Website.Services.Interfaces;

namespace Website.Services.FIlters;

public class GpuFilterService(IMemoryCache cache) : BaseFilterService, IFilterService
{
    public Dictionary<string, List<string>> Filter(string selectedFilters)
    {
        var result = new Dictionary<string, List<string>>();
        var filterModel = JsonConvert.DeserializeObject<GpuFilterModel>(selectedFilters);

        return result;
    }
}
