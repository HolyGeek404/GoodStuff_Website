using GoodStuff_DomainModels.Models.Products;
using Microsoft.AspNetCore.Components;
using Website.Services.Other;

namespace Website.Components.Products.All;

public partial class BaseProduct : ComponentBase
{
    [Parameter] public IEnumerable<BaseProductModel> ProductModelList { get; set; }
    [Parameter] public string Category { get; set; }
    [Inject] private IComponentResolver ComponentResolver { get; set; }
    public Type ProductType { get; set; }
    protected override void OnParametersSet()
    {
        ProductType = ComponentResolver.Resolve(Category);
    }
    private static Dictionary<string, object> GetParameters(BaseProductModel product)
    {
        return new Dictionary<string, object>
        {
            { "Model", product }
        };
    }
}