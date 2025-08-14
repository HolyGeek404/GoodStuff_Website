using GoodStuff_DomainModels.Models.Products;

namespace Website.Services.Product.ViewBuilder;

public class CoolerBaseViewBuilder: BaseViewBuilder<CoolerModel>
{
    protected override void AddBaseParams(CoolerModel model)
    {
        View.Append("<div class=\"col-12 text mt-3 p-0 m-0 text-left text-black-50\">");
        View.Append($"<span class=\"d-block text\"><b>Fans:</b> {model.Fans}</span>");
        View.Append($"<span class=\"d-block text\"><b>RPM Control:</b> {model.RpmControl}</span>");
        View.Append($"<span class=\"d-block text\"><b>Compatibility:</b> {model.Compatibility}</span>");
        View.Append($"<span class=\"d-block text\"><b>Heat Pipes:</b> {model.HeatPipes}</span>");
        View.Append("</div>");
    }
    
    public string BuildFull(List<CpuModel> CoolerModel)
    {
        return "not ready yet";
    }
}