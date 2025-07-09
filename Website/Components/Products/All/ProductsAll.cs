using Microsoft.AspNetCore.Components;
using Website.Services.Interfaces;

namespace Website.Components.Products.All;

public partial class ProductsAll : ComponentBase
{
    [Inject] IProductService productService { get; set; }
    [Inject] IProductFilterService productFilterService { get; set; }
    [Parameter] public string Category { get; set; }
    [SupplyParameterFromForm] public string selectedFilters { get; set; }

    private IProductFilterService filterService { get; set; }
    public List<Dictionary<string, string>> Model { get; set; }
    public Dictionary<string, List<string>> Filters { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await productService.GetModel(Category);
        Filters = productFilterService.GetFilters(Model, Category);
    }

    private async Task Filter()
    {
        var filteredProducts = productFilterService.Filter(Model,selectedFilters, Category);
    }
}