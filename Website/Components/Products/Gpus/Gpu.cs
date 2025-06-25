using Website.Api;
using Microsoft.AspNetCore.Components;
using Website.Models.Products;

namespace Website.Components.Products.Gpus;

public partial class Gpu : ComponentBase
{
    [Inject]
    public GoodStuffProductApiClient ApiClient { get; set; }

    private List<GpuModel>? gpuModelList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await ApiClient.GetGpuProducts();
        if (result.Success)
        {
            gpuModelList = (List<GpuModel>)result.Content!;
        }
    }
}