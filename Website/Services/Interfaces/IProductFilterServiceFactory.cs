using Website.Services.Interfaces;

namespace Website.Factories;

public interface IProductFilterServiceFactory
{
    IProductFilterService Get(string type);
}