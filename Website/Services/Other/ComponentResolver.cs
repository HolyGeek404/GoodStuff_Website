using Website.Components.Products.All;

namespace Website.Services.Other;

public class ComponentResolver : IComponentResolver
{
    private readonly IDictionary<string, Type> _map = new Dictionary<string, Type>
    {
        { "GPU", typeof(Gpu) },
        { "CPU", typeof(Cpu) }
    };

    public Type Resolve(string key)
    {
        return _map.TryGetValue(key, out var type) ? type : null;
    }
}