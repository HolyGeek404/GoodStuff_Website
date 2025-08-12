using GoodStuff_DomainModels.Models.Products;
using Newtonsoft.Json;
using Website.Services.Interfaces;

namespace Website.Services.Product;

public class GpuProductDeserializer : IProductDeserializer
{
    
    public T DeserializeToSingle<T>(object content)
    {
        throw new NotImplementedException();
    }

    public List<T> DeserializeToList<T>(object content)
    {
        return (List<T>)JsonConvert.DeserializeObject(content.ToString() ?? string.Empty);
    }
}