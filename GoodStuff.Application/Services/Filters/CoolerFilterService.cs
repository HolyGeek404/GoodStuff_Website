using GoodStuff_DomainModels.Models.Products;
using GoodStuff.Application.Services.Interfaces;

namespace GoodStuff.Application.Services.Filters;

public class CoolerFilterService : IProductFilterService
{
    public List<BaseProductModel> Filter(IEnumerable<BaseProductModel> productList,
        Dictionary<string, List<string>> selectedFilters)
    {
        var coolerList = productList.OfType<CoolerModel>();

        var fans = selectedFilters.GetValueOrDefault("Fans")?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var rpmControl = selectedFilters.GetValueOrDefault("RPMControll")?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var compatibility = selectedFilters.GetValueOrDefault("Compatibility")
            ?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var heatPipes = selectedFilters.GetValueOrDefault("HeatPipes")?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var teams = selectedFilters.GetValueOrDefault("Team")?.ToHashSet(StringComparer.OrdinalIgnoreCase);
        decimal? minPrice = null;

        if (selectedFilters.TryGetValue("MinPrice", out var minList) &&
            decimal.TryParse(minList.FirstOrDefault(), out var mp))
            minPrice = mp;

        decimal? maxPrice = null;
        if (selectedFilters.TryGetValue("MaxPrice", out var maxList) &&
            decimal.TryParse(maxList.FirstOrDefault(), out var xp))
            maxPrice = xp;

        var filtered = coolerList.Where(cpu =>
            (fans == null || fans.Count == 0 || fans.Contains(cpu.Fans)) &&
            (rpmControl == null || rpmControl.Count == 0 || rpmControl.Contains(cpu.RpmControl)) &&
            (compatibility == null || compatibility.Count == 0 ||
             compatibility.Contains(cpu.Compatibility.ToString())) &&
            (heatPipes == null || heatPipes.Count == 0 || heatPipes.Contains(cpu.HeatPipes)) &&
            (teams == null || teams.Count == 0 || teams.Contains(cpu.Team)) &&
            (!minPrice.HasValue || int.Parse(cpu.Price) >= minPrice.Value) &&
            (!maxPrice.HasValue || int.Parse(cpu.Price) <= maxPrice.Value));
        return filtered.Cast<BaseProductModel>().ToList();
    }

    public Dictionary<string, List<string>> GetFilters(IEnumerable<BaseProductModel> productList)
    {
        var coolers = productList.OfType<CoolerModel>();

        List<string> ExtractFilters(Func<CoolerModel, string> selector)
        {
            return coolers
                .Select(selector)
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        return new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Fans"] = ExtractFilters(g => g.Fans),
            ["RPMControll"] = ExtractFilters(g => g.RpmControl),
            ["Compatibility"] = ExtractFilters(g => g.Compatibility.ToString()),
            ["HeatPipes"] = ExtractFilters(g => g.HeatPipes),
            ["Team"] = ExtractFilters(g => g.Team)
        };
    }
}