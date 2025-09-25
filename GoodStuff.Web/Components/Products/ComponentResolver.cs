using GoodStuff_DomainModels.Models.Enums;
using GoodStuff.Application.Services.Interfaces;
using GoodStuff.Web.Components.Products.All;

namespace GoodStuff.Web.Components.Products;

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