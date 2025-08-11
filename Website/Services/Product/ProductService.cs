using Microsoft.Extensions.Caching.Memory;
using Website.Api;
using Website.Services.Interfaces;

namespace Website.Services.Product;

public abstract class ProductService<T>(BaseProductApiClient baseProductApiClient,
                                        IMemoryCache cache) where T : class
{
    public abstract Task<List<T>> GetModel(string category);
}