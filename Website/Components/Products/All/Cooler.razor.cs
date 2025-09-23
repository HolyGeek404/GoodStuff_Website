using GoodStuff_DomainModels.Models.Products;
using Microsoft.AspNetCore.Components;

namespace Website.Components.Products.All;

public partial class Cooler : ComponentBase
{
    [Parameter] public CoolerModel Model { get; set; }
}