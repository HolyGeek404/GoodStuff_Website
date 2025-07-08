using Website.Api;
using Microsoft.AspNetCore.Components;
using Website.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Website.Components.Products.All;

public partial class ProductsAll : ComponentBase
{
    [Inject]
    private GoodStuffProductApiClient ApiClient { get; set; }
    [Inject]
    private IMemoryCache cache { get; set; }

    private IFilterService filterService { get; set; }

    [Parameter]
    public string Category { get; set; }

    public List<Dictionary<string, string>> Model { get; set; }
    public Dictionary<string, List<string>> Filters { get; set; }

    protected override async Task OnInitializedAsync()
    {
        
        
    }

    private async Task Filter()
    {

    }
}