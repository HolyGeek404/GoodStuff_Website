using GoodStuff_DomainModels.Models.Products;

namespace Website.Services.Product;

public class GpuViewBuilder: BaseViewBuilder<Gpu>
{
    protected override void AddBaseParams(Gpu model)
    {
        _view.Append("<div class=\"col-12 text mt-3 p-0 m-0 text-left text-black-50\">");
        _view.Append($"<span class=\"d-block text\"><b>Recommended PSU:</b> {model.RecommendedPSUPower}</span>");
        _view.Append($"<span class=\"d-block text\"><b>Memory Bus:</b> {model.MemoryBus}</span>");
        _view.Append($"<span class=\"d-block text\"><b>Core Ratio:</b> {model.CoreRatio}</span>");
        _view.Append("</div>");
    }
    
    public string BuildFull(List<Gpu> gpu)
    {
        return "not ready yet";
    }
}