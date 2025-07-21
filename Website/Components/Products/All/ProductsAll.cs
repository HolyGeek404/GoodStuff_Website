using Microsoft.AspNetCore.Components;
using Website.Services.Interfaces;

namespace Website.Components.Products.All;

public partial class ProductsAll : ComponentBase
{
    [Inject] IProductService ProductService { get; set; }
    [Inject] IProductFilterService ProductFilterService { get; set; }
    [Parameter] public string Category { get; set; }

    public Dictionary<string, List<string>> SelectedFilters = [];
    public List<Dictionary<string, string>> Model { get; set; }
    public List<Dictionary<string, string>> MatchedProducts { get; set; }
    public Dictionary<string, List<string>> Filters { get; set; }
    public bool AreFiltersCleared = false;
    protected override async Task OnInitializedAsync()
    {
        Model = await ProductService.GetModel(Category);
        MatchedProducts = Model;
        Filters = ProductFilterService.GetFilters(Model, Category);
    }

    private void UpdateFilters(string type, string value, ChangeEventArgs e)
    {
        if (SelectedFilters.TryGetValue(type, out var filterList))
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
                    SelectedFilters.Remove(type);
                }
            }
        }
        else
        {
            SelectedFilters.Add(type, [value]);
        }
    }

    private void Filter()
    {
        MatchedProducts = ProductFilterService.Filter(Model, SelectedFilters, Category);
    }

    private void ClearFilters()
    {
        SelectedFilters.Clear();
        // its stupid, i know but it works
        AreFiltersCleared = !AreFiltersCleared;
        Filters = ProductFilterService.GetFilters(Model, Category);
        MatchedProducts = Model;
    }
}