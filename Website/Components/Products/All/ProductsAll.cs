using Website.Api;
using Microsoft.AspNetCore.Components;
using Website.Services.Interfaces;

namespace Website.Components.Products.All;

public partial class ProductsAll : ComponentBase
{
    [Inject]
    private GoodStuffProductApiClient ApiClient { get; set; }

    [Inject]
    private IFilterService filterService { get; set; }

    [Parameter]
    public string Category { get; set; }

    public List<Dictionary<string, string>> Model { get; set; }
    public Dictionary<string, List<string>> Filters { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await ApiClient.GetAllProductsByType(Category);
        if (result.Success)
        {
            Model = (List<Dictionary<string, string>>)result.Content!;
            Filters = filterService.CreateFilters(Model, Category);
        }
    }
}