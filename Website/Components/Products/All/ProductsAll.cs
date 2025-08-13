using GoodStuff_DomainModels.Models.Products;
using Microsoft.AspNetCore.Components;
using Website.Services.Factories;
using Website.Services.Interfaces;
using Website.Services.Product;

namespace Website.Components.Products.All;

public partial class ProductsAll : ComponentBase
{
    [Inject] private IProductServiceFactory ProductServiceFactory { get; set; }
    [Parameter] public string Category { get; set; }
    private IProductService ProductService { get; set; }
    private readonly Dictionary<string, List<string>> _selectedFilters = [];
    private bool _areFiltersClear;
    private MarkupString Preview { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        ProductService = ProductServiceFactory.Get(Category);
        var products = await ProductService.GetModel(Category);
        var viewBuilder = new GpuViewBuilder();
        Preview = viewBuilder.BuildPreview((IEnumerable<Gpu>)products);
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

    // private void Filter()
    // {
    //     MatchedProducts = ProductFilterService.Filter(Model, _selectedFilters, Category);
    // }
    //
    // private void ClearFilters()
    // {
    //     _selectedFilters.Clear();
    //     _areFiltersClear = !_areFiltersClear;
    //     Filters = ProductFilterService.GetFilters(Model, Category);
    //     MatchedProducts = Model;
    // }
}