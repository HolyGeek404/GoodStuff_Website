namespace Website.Services.Interfaces;

public interface IProductService
{
    Task<object> GetModel(string category);
}