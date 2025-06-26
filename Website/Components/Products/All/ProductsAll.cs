using Website.Api;
using Microsoft.AspNetCore.Components;

namespace Website.Components.Products.All;

public partial class ProductsAll : ComponentBase
{
    [Inject]
    private GoodStuffProductApiClient ApiClient { get; set; }

    [Parameter]
    public string Category { get; set; }
    public List<Dictionary<string, string>>? Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await ApiClient.GetAllProductsByType(Category);
        if (result.Success)
        {
            Model = (List<Dictionary<string, string>>)result.Content!;
        }
    }
}