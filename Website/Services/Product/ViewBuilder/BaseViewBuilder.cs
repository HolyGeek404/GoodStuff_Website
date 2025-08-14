using System.Text;
using GoodStuff_DomainModels.Models.Products;
using Microsoft.AspNetCore.Components;

namespace Website.Services.Product.ViewBuilder;

public abstract class BaseViewBuilder<TProduct> : IViewBuilder where TProduct : BaseProductModel
{
    protected readonly StringBuilder View = new();
        
    public MarkupString BuildPreview(IEnumerable<BaseProductModel> model)
    {
        foreach (var product in model)
        {
            AddBaseInfo(product as TProduct);
            AddBaseParams(product as TProduct);
            AddPrice(product as TProduct);
        }
        return (MarkupString)View.ToString();
    }

    private void AddBaseInfo(TProduct model)
    {
        View.Append($"<a href=\"/Product/{model.Category}/{model.ProductId}\">");
        View.Append("<div class=\"col-lg-10 col-8 shadow-sm rounded m-2\" style=\"height:400px;width:450px;\">");
        View.Append("<div class=\"col-12 p-0 m-1\">");
        View.Append($"<img src=\"{model.ProductImg}\" width=\"230\" height=\"200\" alt=\"ProductImg\" />");
        View.Append("</div>");
        View.Append("<div class=\"col-12 text p-0 m-0\">");
        View.Append($"<h6>{model.Name}</h6>");
        View.Append("</div>");
    }
    protected abstract void AddBaseParams(TProduct model);

    private void AddPrice(TProduct model)
    {
        View.Append("<div class=\"col-12 text-center mt-4 m-1\">");
        View.Append($"<h4>{model.Price} zl</h4>");
        View.Append("</div>");
        View.Append("</div>");
        View.Append("</a>");
    }
}