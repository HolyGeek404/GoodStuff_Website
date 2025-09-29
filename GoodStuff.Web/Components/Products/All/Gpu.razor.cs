using GoodStuff_DomainModels.Models.Products;
using Microsoft.AspNetCore.Components;

namespace GoodStuff.Web.Components.Products.All;

public partial class Gpu : ComponentBase
{
    [Parameter] public GpuModel Model { get; set; }
}