namespace Website.Services;

public static class ProductInfoExtractorService
{
    private static readonly Dictionary<string, string[]> CategoryProperties = new()
    {
        { "GPU", new[] { "RecommendedPSUPower", "MemoryBus", "CoreRatio" } },
        { "CPU", new[] { "Architecture", "TDP", "Socket" } },
        { "COOLER", new[] { "Fans", "RPMControll", "Compatibility", "HeatPipes" } },
    };

    public static Dictionary<string, string> ExtractBasicInfo(Dictionary<string, string> model, string category)
    {
        if (model == null || string.IsNullOrWhiteSpace(category))
            return [];

        if (!CategoryProperties.TryGetValue(category.ToUpperInvariant(), out var props))
            return [];

        var result = new Dictionary<string, string>();

        foreach (var propName in props)
        {
            if (model.TryGetValue(propName, out var value) && !string.IsNullOrEmpty(value))
            {
                result[propName] = value;
            }
        }
        return result;
    }
}