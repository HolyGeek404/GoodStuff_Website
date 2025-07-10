using Microsoft.AspNetCore.Components;
using Website.Services.Interfaces;

namespace Website.Components.Products.All;

public partial class ProductsAll : ComponentBase
{
    [Inject] IProductService productService { get; set; }
    [Inject] IProductFilterService productFilterService { get; set; }
    [Parameter] public string Category { get; set; }
    public Dictionary<string, List<string>> selectedFilters = [];

    private IProductFilterService filterService { get; set; }
    public List<Dictionary<string, string>> Model { get; set; }
    public Dictionary<string, List<string>> Filters { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await productService.GetModel(Category);
        Filters = productFilterService.GetFilters(Model, Category);
    }

    private void UpdateFilters(string type, string value, ChangeEventArgs e)
    {
        if (selectedFilters.TryGetValue(type, out var filterList))
        {
            if ((bool)e.Value!)
            {
                if (!filterList.Contains(value))
                {
                    filterList.Add(value);
                }
            }
            else
            {
                filterList.Remove(value);
            }
        }
        else
        {
            selectedFilters.Add(type, [value]);
        }
    }

    private void Filter()
    {
        // var filteredProducts = productFilterService.Filter(Model,selectedFilters, Category);
    }
}