using Website.Services.Product.ViewBuilder;

namespace Website.Services.Interfaces;

public interface IViewBuilderFactory
{
    IViewBuilder Get(string category);
}