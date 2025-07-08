using Website.Services.Interfaces;

namespace Website.Factories.Interfaces;

public interface IFilterServiceFactory
{
    IFilterService? Get(string category);
}