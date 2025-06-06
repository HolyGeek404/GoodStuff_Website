using GoodStuff_Blazor.Models;

namespace GoodStuff_Blazor.Components.Products;

public partial class Gpu
{
    private List<GpuModel> gpuModels { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await ApiClient.GetGpuProducts();
        if (result.Success)
        {
            gpuModels = (List<GpuModel>)result.Content;
        }
    }
}