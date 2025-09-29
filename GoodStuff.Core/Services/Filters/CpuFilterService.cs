using GoodStuff_DomainModels.Models.Products;
using GoodStuff.Core.Services.Interfaces;

namespace GoodStuff.Core.Services.Filters;

public class CpuFilterService : IProductFilterService
{
    public List<BaseProductModel> Filter(IEnumerable<BaseProductModel> productList,
        Dictionary<string, List<string>> selectedFilters)
    {
        var cpus = productList.OfType<CpuModel>();

        var sockets = selectedFilters.GetValueOrDefault("Socket")?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var architectures = selectedFilters.GetValueOrDefault("Architecture")
            ?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var unlockedMultiplayer = selectedFilters.GetValueOrDefault("UnlockedMultiplayer")
            ?.ToHashSet(StringComparer.OrdinalIgnoreCase);
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
            (sockets == null || sockets.Count == 0 || sockets.Contains(cpu.Socket)) &&
            (architectures == null || architectures.Count == 0 || architectures.Contains(cpu.Architecture)) &&
            (unlockedMultiplayer == null || unlockedMultiplayer.Count == 0 ||
             unlockedMultiplayer.Contains(cpu.UnlockedMultiplayer.ToString())) &&
            (teams == null || teams.Count == 0 || teams.Contains(cpu.Team)) &&
            (!minPrice.HasValue || int.Parse(cpu.Price) >= minPrice.Value) &&
            (!maxPrice.HasValue || int.Parse(cpu.Price) <= maxPrice.Value));
        return filtered.Cast<BaseProductModel>().ToList();
    }

    public Dictionary<string, List<string>> GetFilters(IEnumerable<BaseProductModel> productList)
    {
        var cpus = productList.OfType<CpuModel>();

        List<string> ExtractFilters(Func<CpuModel, string> selector)
        {
            return cpus
                .Select(selector)
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        return new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Socket"] = ExtractFilters(g => g.Socket),
            ["Architecture"] = ExtractFilters(g => g.Architecture),
            ["UnlockedMultiplayer"] = ExtractFilters(g => g.UnlockedMultiplayer.ToString()),
            ["Team"] = ExtractFilters(g => g.Team)
        };
    }
}