namespace Website.Services.Filters;

public static class FilterCategories
{
    private static readonly List<string> GpuFilters =
    [
        "Manufacturer","GpuProcessorName","MemorySize","MemoryType","Team"
    ];
    private static readonly List<string> CpuFilters =
    [
        "Socket","Architecture","UnlockedMultipler","Team"
    ];
    private static readonly List<string> CoolerFilters =
    [
        "Manufactures","CoolerTypes"
    ];
    
    public static List<string> Get(string category)
    {
        return category switch
        {
            "GPU" => GpuFilters,
            "CPU" => CpuFilters,
            "Cooler" => CoolerFilters,
            _ => []
        };
    }
}