using Microsoft.AspNetCore.Components;
using Website.Api;

namespace Website.Components.Products.Single;

public partial class ProductSingle
{
    [Inject]
    public GoodStuffProductApiClient ApiClient { get; set; }

    [Parameter]
    public string Id { get; set; }

    [Parameter]
    public string Category { get; set; }

    private Dictionary<string, string>? Model { get; set; }
    private string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await ApiClient.GetSingleProductById(Category, Id);
        if (result.Success && result.Content is List<Dictionary<string, string>> list && list.Count > 0)
        {
            Model = list[0];
        }
        else
        {
            ErrorMessage = result.ErrorMessage ?? "Product not found.";
        }
    }

    private static readonly HashSet<string> ExcludedProperties =
    [
        "ProductId", "Name", "ProductImg", "Category"
    ];
}