using Microsoft.AspNetCore.Components;
using Website.Services.Interfaces;
using Website.Services.Product;

namespace Website.Components.Products.All;

public partial class ProductsAll : ComponentBase
{
    [Inject] private IProductService ProductService { get; set; }
    [Inject] private IProductFilterService ProductFilterService { get; set; }
    [Parameter] public string Category { get; set; }
    private GpuViewBuilder viewBuilder = new GpuViewBuilder();
    private readonly Dictionary<string, List<string>> _selectedFilters = [];
    private List<Dictionary<string, string>> Model { get; set; }
    private List<Dictionary<string, string>> MatchedProducts { get; set; }
    private Dictionary<string, List<string>> Filters { get; set; }
    private bool _areFiltersClear;
    protected override async Task OnParametersSetAsync()
    {
        Model = await ProductService.GetModel(Category);
        MatchedProducts = Model;
        Filters = ProductFilterService.GetFilters(Model, Category);
    }

    private void UpdateFilters(string type, string value, ChangeEventArgs e)
    {
        if (_selectedFilters.TryGetValue(type, out var filterList))
        {
            if (e.Value != null && (bool)e.Value)
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
                    _selectedFilters.Remove(type);
                }
            }
        }
        else
        {
            _selectedFilters.Add(type, [value]);
        }
    }

    private void Filter()
    {
        MatchedProducts = ProductFilterService.Filter(Model, _selectedFilters, Category);
    }

    private void ClearFilters()
    {
        _selectedFilters.Clear();
        _areFiltersClear = !_areFiltersClear;
        Filters = ProductFilterService.GetFilters(Model, Category);
        MatchedProducts = Model;
    }
}