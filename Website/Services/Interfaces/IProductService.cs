namespace Website.Services.Interfaces;

public interface IProductService
{
    Task<List<Dictionary<string, string>>> GetModel(string category);
}