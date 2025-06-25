
using Microsoft.AspNetCore.Components;
using Website.Api;
using Website.Models;

namespace Website.Components.Products.Cpus;

public partial class Cpu
{
    [Inject]
    public GoodStuffProductApiClient ApiClient { get; set; }

    private List<CpuModel>? cpuModelList { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var result = await ApiClient.GetCpuProducts();
        if (result.Success)
        {
            cpuModelList = (List<CpuModel>)result.Content!;
        }
    }
}
