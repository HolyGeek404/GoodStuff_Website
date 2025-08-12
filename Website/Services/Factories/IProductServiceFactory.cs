using Website.Services.Interfaces;

namespace Website.Services.Factories;

public interface IProductServiceFactory
{
    IProductService Get(string type);
}