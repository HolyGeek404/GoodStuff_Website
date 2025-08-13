using System.Text;
using GoodStuff_DomainModels.Models.Products;
using Microsoft.AspNetCore.Components;

namespace Website.Services.Product;

public abstract class BaseViewBuilder<TProduct> where TProduct : BaseProduct
{
    protected readonly StringBuilder _view = new();
    
    public MarkupString BuildPreview(IEnumerable<TProduct> model)
    {
        foreach (var product in model)
        {
            AddBaseInfo(product);
            AddBaseParams(product);
            AddPrice(product);
        }
        return (MarkupString)_view.ToString();
    }

    private void AddBaseInfo(TProduct model)
    {
        _view.Append($"<a href=\"/Product/{model.Category}/{model.ProductId}\">");
        _view.Append("<div class=\"col-lg-10 col-8 shadow-sm rounded m-2\" style=\"height:400px;width:450px;\">");
        _view.Append("<div class=\"col-12 p-0 m-1\">");
        _view.Append($"<img src=\"{model.ProductImg}\" width=\"230\" height=\"200\" alt=\"ProductImg\" />");
        _view.Append("</div>");
        _view.Append("<div class=\"col-12 text p-0 m-0\">");
        _view.Append($"<h6>{model.Name}</h6>");
        _view.Append("</div>");
    }
    protected abstract void AddBaseParams(TProduct model);

    private void AddPrice(TProduct model)
    {
        _view.Append("<div class=\"col-12 text-center mt-4 m-1\">");
        _view.Append($"<h4>{model.Price} zl</h4>");
        _view.Append("</div>");
        _view.Append("</div>");
        _view.Append("</a>");
    }
}