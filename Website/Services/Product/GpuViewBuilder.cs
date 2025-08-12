using System.Text;
using GoodStuff_DomainModels.Models.Products;

namespace Website.Services.Product;

public class GpuViewBuilder 
{
    public string BuildPreview(List<Gpu> gpuList)
    {
        var view = new StringBuilder();
       
        foreach (var gpu in gpuList)
        {
            view.Append($"<a href=\"/Product/{gpu.Category}/{gpu.ProductId}\">");
            view.Append("<div class=\"col-lg-10 col-8 shadow-sm rounded m-2\" style=\"height:400px;width:450px;\">");
            view.Append("<div class=\"col-12 p-0 m-1\">");
            view.Append($"<img src=\"{gpu.ProductImg}\" width=\"230\" height=\"200\" alt=\"ProductImg\" />");
            view.Append("</div>");
            view.Append("<div class=\"col-12 text p-0 m-0\">");
            view.Append($"<h6>{gpu.Name}</h6>");
            view.Append("</div>");
            view.Append("<div class=\"col-12 text mt-3 p-0 m-0 text-left text-black-50\">");
            view.Append($"<span class=\"d-block text\"><b>{nameof(gpu.RecommendedPSUPower)}:</b> {gpu.RecommendedPSUPower}</span>");
            view.Append($"<span class=\"d-block text\"><b>{nameof(gpu.MemoryBus)}:</b> {gpu.MemoryBus}</span>");
            view.Append($"<span class=\"d-block text\"><b>{nameof(gpu.CoreRatio)}:</b> {gpu.CoreRatio}</span>");
            view.Append("</div>");
            view.Append("<div class=\"col-12 text-center mt-4 m-1\">");
            view.Append($"<h4>{gpu.Price} zl</h4>");
            view.Append("</div>");
            view.Append("</div>");
            view.Append("</a>");
        }
        
        return view.ToString();
    }

    public string BuildFull(List<Gpu> gpu)
    {
        return "";
    }
}