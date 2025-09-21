using GoodStuff_DomainModels.Models.Products;
using Microsoft.AspNetCore.Components;

namespace Website.Components.Products.All;

public partial class BaseProduct : ComponentBase
{
    [Parameter]
    public IEnumerable<BaseProductModel> ProductModelList { get; set; }
}