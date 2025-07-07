using System;

namespace Website.Services;

public static class FilterCategories
{
    private static readonly List<string> GpuFilters =
    [
        "Manufacture","AmdGpuProcessorNames","NvidiaGpuProcessorNames","MemorySizes","MemoryTypes","TeamList"
    ];
    private static readonly List<string> CpuFilters =
    [
        "SocketList","ArchitectureList","UnlockedMultiplier","TeamList"
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
            _ => new List<string>()
        };
    }
}