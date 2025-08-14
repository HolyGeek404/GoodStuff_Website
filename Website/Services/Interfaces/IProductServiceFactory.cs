namespace Website.Services.Interfaces;

public interface IProductServiceFactory
{
    IProductService Get(string type);
}