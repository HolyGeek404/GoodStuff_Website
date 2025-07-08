using Microsoft.AspNetCore.Components;

namespace Website.Components.Products.All;

public partial class ProductsAll : ComponentBase
{
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