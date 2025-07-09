using Newtonsoft.Json;
using Website.Models.Filters;
using Website.Services.Interfaces;

namespace Website.Services.FIlters;

public class GpuFilterService : BaseFilterService, IProductFilterService
{
    public List<Dictionary<string, string>> Filter(List<Dictionary<string, string>> model, string selectedFilters, string category)
    {
        if (model == null || string.IsNullOrEmpty(selectedFilters))
            return model ?? [];

        try
        {
            var filterModel = JsonConvert.DeserializeObject<GpuFilterModel>(selectedFilters);
            if (filterModel == null)
                return model;

            var hasManufacturers = filterModel.Manufacturer?.Count > 0;
            var hasGpuProcessorName = filterModel.GpuProcessorName?.Count > 0;
            var hasTeamList = filterModel.Team?.Count > 0;
            var hasMemorySizes = filterModel.MemorySize?.Count > 0;
            var hasMemoryTypes = filterModel.MemoryType?.Count > 0;

            var filteredList = model
                .Where(gpu =>
                {
                    // Null safety checks for dictionary access
                    if (gpu == null) return false;

                    // Manufacturer filter
                    if (hasManufacturers &&
                        (!gpu.ContainsKey("Manufacturer") ||
                         !filterModel.Manufacturer!.Contains(gpu["Manufacturer"])))
                        return false;

                    // GPU Processor Name filter
                    if (hasGpuProcessorName &&
                        (!gpu.ContainsKey("GpuProcessorName") ||
                         !filterModel.GpuProcessorName!.Contains(gpu["GpuProcessorName"])))
                        return false;

                    // Team filter
                    if (hasTeamList &&
                        (!gpu.ContainsKey("Team") ||
                         !filterModel.Team!.Contains(gpu["Team"])))
                        return false;

                    // Memory Size filter
                    if (hasMemorySizes &&
                        (!gpu.ContainsKey("MemorySize") ||
                         !filterModel.MemorySize!.Contains(gpu["MemorySize"])))
                        return false;

                    // Memory Type filter
                    if (hasMemoryTypes &&
                        (!gpu.ContainsKey("MemoryType") ||
                         !filterModel.MemoryType!.Contains(gpu["MemoryType"])))
                        return false;

                    // Price range filters
                    if (gpu.ContainsKey("Price") && !string.IsNullOrEmpty(gpu["Price"]))
                    {
                        if (int.TryParse(gpu["Price"], out int price))
                        {
                            if (filterModel.PriceMin > 0 && price < filterModel.PriceMin)
                                return false;

                            if (filterModel.PriceMax > 0 && price > filterModel.PriceMax)
                                return false;
                        }
                        else
                        {
                            // If price can't be parsed and price filters are set, exclude this item
                            if (filterModel.PriceMin > 0 || filterModel.PriceMax > 0)
                                return false;
                        }
                    }
                    else
                    {
                        // If no price data and price filters are set, exclude this item
                        if (filterModel.PriceMin > 0 || filterModel.PriceMax > 0)
                            return false;
                    }

                    return true;
                })
                .ToList();

            return filteredList;
        }
        catch (JsonException)
        {
            return model;
        }
    }
}