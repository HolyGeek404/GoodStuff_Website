using Microsoft.Extensions.Caching.Memory;

namespace Website.Services.Product;

public abstract class ProductService<T>(IMemoryCache cache) where T : class
{
    public abstract Task<List<T>> GetModel(string category);
}