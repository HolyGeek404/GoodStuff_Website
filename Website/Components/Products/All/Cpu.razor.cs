using GoodStuff_DomainModels.Models.Products;
using Microsoft.AspNetCore.Components;

namespace Website.Components.Products.All;

public partial class Cpu : ComponentBase
{
    [Parameter] public CpuModel Model { get; set; }
}