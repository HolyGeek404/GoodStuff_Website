using GoodStuff_DomainModels.Models.Products;

namespace Website.Services.Product.ViewBuilder;

public class GpuBaseViewBuilder: BaseViewBuilder<GpuModel>
{
    protected override void AddBaseParams(GpuModel model)
    {
        View.Append("<div class=\"col-12 text mt-3 p-0 m-0 text-left text-black-50\">");
        View.Append($"<span class=\"d-block text\"><b>Recommended PSU:</b> {model.RecommendedPsuPower}</span>");
        View.Append($"<span class=\"d-block text\"><b>Memory Bus:</b> {model.MemoryBus}</span>");
        View.Append($"<span class=\"d-block text\"><b>Core Ratio:</b> {model.CoreRatio}</span>");
        View.Append("</div>");
    }
    
    public string BuildFull(List<GpuModel> gpu)
    {
        return "not ready yet";
    }
}