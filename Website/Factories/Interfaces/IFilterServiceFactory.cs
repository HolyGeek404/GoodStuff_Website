using Website.Services.Interfaces;

namespace Website.Factories.Interfaces;

public interface IFilterServiceFactory
{
    IProductFilterService? Get(string category);
}