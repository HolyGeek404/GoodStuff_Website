using GoodStuff_DomainModels.Models.Enums;
using Website.Components.Products.All;
using Website.Services.Interfaces;

namespace Website.Services.Other;

public class ComponentResolver : IComponentResolver
{
    private readonly Dictionary<ProductCategories, Type> _map = new()
    {
        { ProductCategories.Gpu, typeof(Gpu) },
        { ProductCategories.Cpu, typeof(Cpu) },
        { ProductCategories.Cooler, typeof(Cooler) }
    };

    public Type Resolve(ProductCategories key)
    {
        return _map.GetValueOrDefault(key);
    }
}