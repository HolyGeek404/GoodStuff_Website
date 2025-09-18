using Autofac.Features.Indexed;
using Website.Services.Interfaces;
using Website.Services.Product.ViewBuilder;

namespace Website.Factories;

public class ViewBuilderFactory(IIndex<string, IViewBuilder> viewBuilders) : IViewBuilderFactory
{
    public IViewBuilder Get(string category)
    {
        return viewBuilders.TryGetValue(category, out var builder) ? builder : null;
    }
}