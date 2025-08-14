using GoodStuff_DomainModels.Models.Products;

namespace Website.Services.Product.ViewBuilder;

public class CpuBaseViewBuilder: BaseViewBuilder<CpuModel>
{
    protected override void AddBaseParams(CpuModel model)
    {
        View.Append("<div class=\"col-12 text mt-3 p-0 m-0 text-left text-black-50\">");
        View.Append($"<span class=\"d-block text\"><b>Architecture:</b> {model.Architecture}</span>");
        View.Append($"<span class=\"d-block text\"><b>TDP:</b> {model.Tdp}</span>");
        View.Append($"<span class=\"d-block text\"><b>Socket:</b> {model.Socket}</span>");
        View.Append("</div>");
    }
    
    public string BuildFull(List<CpuModel> Cpu)
    {
        return "not ready yet";
    }
}