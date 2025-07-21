using Website.Factories.Interfaces;
using Website.Services.Filters;
using Website.Services.Interfaces;

namespace Website.Factories;

public class FilterServiceFactory() : IFilterServiceFactory
{
    public IProductFilterService? Get(string category)
    {
        return category.ToLower() switch
        {
            "gpu" => new GpuFilterService(),
            _ => null,
        };
    }
}