using GoodStuff_DomainModels.Models.Enums;
using GoodStuff_DomainModels.Models.Products;
using GoodStuff.Application.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace GoodStuff.Web.Components.Products.All;

public partial class ProductsAll : ComponentBase
{
    private readonly Dictionary<string, List<string>> _selectedFilters = [];
    private bool _areFiltersClear;
    [Inject] private IProductServiceFactory ProductServiceFactory { get; set; }

    [Parameter]
    public string CategoryString
    {
        get => Category.ToString();
        set
        {
            if (Enum.TryParse<ProductCategories>(value, true, out var enumValue)) Category = enumValue;
        }
    }

    private ProductCategories Category { get; set; }

    private IProductService ProductService { get; set; }
    private IEnumerable<BaseProductModel> ProductList { get; set; }
    private Dictionary<string, List<string>> AvailableFilters { get; set; }


    protected override async Task OnParametersSetAsync()
    {
        ProductList = null; // force "Loading..." state
        StateHasChanged();  // trigger re-render with null list

        ProductService = ProductServiceFactory.Get(Category);
        ProductList = await ProductService.GetProducts();
        AvailableFilters = ProductService.GetFilters(ProductList);
    }


    private void UpdateFilters(string type, string value, ChangeEventArgs e)
    {
        if (_selectedFilters.TryGetValue(type, out var filterList))
        {
            if (e.Value != null && (bool)e.Value)
            {
                if (!filterList.Contains(value)) filterList.Add(value);
            }
            else
            {
                filterList.Remove(value);
                if (filterList.Count == 0) _selectedFilters.Remove(type);
            }
        }
        else
        {
            _selectedFilters.Add(type, [value]);
        }
    }

    private void Filter()
    {
        ProductList = ProductService.FilterProducts(ProductList, _selectedFilters);
    }

    private async Task ClearFilters()
    {
        _selectedFilters.Clear();
        _areFiltersClear = !_areFiltersClear;
        ProductList = await ProductService.GetProducts();
    }
}