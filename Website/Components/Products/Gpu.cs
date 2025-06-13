using Website.Models;

namespace Website.Components.Products;

public partial class Gpu
{
    private List<GpuModel> gpuModelList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await ApiClient.GetGpuProducts();
        if (result.Success)
        {
            gpuModelList = (List<GpuModel>)result.Content;
        }
    }
}