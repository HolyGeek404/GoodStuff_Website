using Microsoft.AspNetCore.Components;
using Website.Services.Interfaces;

namespace Website.Components.Products.All;

public partial class ProductsAll : ComponentBase
{
    [Inject] IProductService productService { get; set; }
    [Inject] IProductFilterService productFilterService { get; set; }
    [Parameter] public string Category { get; set; }

    public Dictionary<string, List<string>> selectedFilters = [];
    public List<Dictionary<string, string>> Model { get; set; }
    public List<Dictionary<string, string>> MatchedProducts { get; set; }
    public Dictionary<string, List<string>> Filters { get; set; }
    public bool AreFiltersCleared = false;
    protected override async Task OnInitializedAsync()
    {
        Model = await productService.GetModel(Category);
        MatchedProducts = Model;
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
                if (filterList.Count == 0)
                {
                    selectedFilters.Remove(type);
                }
            }
        }
        else
        {
            selectedFilters.Add(type, [value]);
        }
    }

    private void Filter()
    {
        MatchedProducts = productFilterService.Filter(Model, selectedFilters, Category);
    }

    private void ClearFilters()
    {
        selectedFilters.Clear();
        // its stupid, i know but it works
        AreFiltersCleared = !AreFiltersCleared;
        Filters = productFilterService.GetFilters(Model, Category);
        MatchedProducts = Model;
    }
}