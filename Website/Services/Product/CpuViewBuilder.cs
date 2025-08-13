using GoodStuff_DomainModels.Models.Products;

namespace Website.Services.Product;

public class CpuViewBuilder: BaseViewBuilder<Cpu>
{
    protected override void AddBaseParams(Cpu model)
    {
        _view.Append("<div class=\"col-12 text mt-3 p-0 m-0 text-left text-black-50\">");
        _view.Append($"<span class=\"d-block text\"><b>Architecture:</b> {model.Architecture}</span>");
        _view.Append($"<span class=\"d-block text\"><b>TDP:</b> {model.TDP}</span>");
        _view.Append($"<span class=\"d-block text\"><b>Socket:</b> {model.Socket}</span>");
        _view.Append("</div>");
    }
    
    public string BuildFull(List<Cpu> Cpu)
    {
        return "not ready yet";
    }
}