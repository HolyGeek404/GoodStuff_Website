using GoodStuff_DomainModels.Models.Products;
using GoodStuff.Website.Application.Services.Interfaces;

namespace GoodStuff.Website.Application.Services.Filters;

public class GpuFilterService : IProductFilterService
{
    public List<BaseProductModel> Filter(IEnumerable<BaseProductModel> productList,
        Dictionary<string, List<string>> selectedFilters)
    {
        var cpus = productList.OfType<GpuModel>();

        var manufacturers = selectedFilters.GetValueOrDefault("Manufacturer")
            ?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var gpuNames = selectedFilters.GetValueOrDefault("GpuProcessorName")
            ?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var memorySizes = selectedFilters.GetValueOrDefault("MemorySize")?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var memoryTypes = selectedFilters.GetValueOrDefault("MemoryType")?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var teams = selectedFilters.GetValueOrDefault("Team")?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        decimal? minPrice = null;

        if (selectedFilters.TryGetValue("MinPrice", out var minList) &&
            decimal.TryParse(minList.FirstOrDefault(), out var mp))
            minPrice = mp;

        decimal? maxPrice = null;
        if (selectedFilters.TryGetValue("MaxPrice", out var maxList) &&
            decimal.TryParse(maxList.FirstOrDefault(), out var xp))
            maxPrice = xp;

        var filtered = cpus.Where(cpu =>
            (manufacturers == null || manufacturers.Count == 0 || manufacturers.Contains(cpu.Manufacturer)) &&
            (gpuNames == null || gpuNames.Count == 0 || gpuNames.Contains(cpu.GpuProcessorName)) &&
            (memorySizes == null || memorySizes.Count == 0 || memorySizes.Contains(cpu.MemorySize.ToString())) &&
            (memoryTypes == null || memoryTypes.Count == 0 || memoryTypes.Contains(cpu.MemoryType)) &&
            (teams == null || teams.Count == 0 || teams.Contains(cpu.Team)) &&
            (!minPrice.HasValue || int.Parse(cpu.Price) >= minPrice.Value) &&
            (!maxPrice.HasValue || int.Parse(cpu.Price) <= maxPrice.Value));
        return filtered.Cast<BaseProductModel>().ToList();
    }

    public Dictionary<string, List<string>> GetFilters(IEnumerable<BaseProductModel> productList)
    {
        var gpus = productList.OfType<GpuModel>();

        List<string> ExtractFilters(Func<GpuModel, string> selector)
        {
            return gpus
                .Select(selector)
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        return new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Manufacturer"] = ExtractFilters(g => g.Manufacturer),
            ["GpuProcessorName"] = ExtractFilters(g => g.GpuProcessorName),
            ["MemorySize"] = ExtractFilters(g => g.MemorySize.ToString()),
            ["MemoryType"] = ExtractFilters(g => g.MemoryType),
            ["Team"] = ExtractFilters(g => g.Team)
        };
    }
}