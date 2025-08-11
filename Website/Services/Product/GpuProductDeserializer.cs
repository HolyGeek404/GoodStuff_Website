using GoodStuff_DomainModels.Models.Products;
using Newtonsoft.Json;
using Website.Services.Interfaces;

namespace Website.Services.Product;

public class GpuProductDeserializer : IProductDeserializer
{
    public T DeserializeToSingle(object content)
    {
        return JsonConvert.DeserializeObject<T>(content.ToString() ?? string.Empty);
    }

    public List<T> DeserializeToList<T>(object content)
    {
        return JsonConvert.DeserializeObject<List<Gpu>>(content.ToString() ?? string.Empty);
    }
}