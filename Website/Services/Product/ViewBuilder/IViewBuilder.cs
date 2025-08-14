using GoodStuff_DomainModels.Models.Products;
using Microsoft.AspNetCore.Components;

namespace Website.Services.Product.ViewBuilder;

public interface IViewBuilder
{
    MarkupString BuildPreview(IEnumerable<BaseProductModel> model);
}