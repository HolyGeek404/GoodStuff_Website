using GoodStuff_DomainModels.Models.Products;
using Newtonsoft.Json;
using Website.Services.Interfaces;

namespace Website.Services.Product;

public class CpuProductDeserializer : IProductDeserializer
{
    public Cpu DeserializeToSingle(object content)
    {
        return JsonConvert.DeserializeObject<Cpu>(content.ToString() ?? string.Empty);
    }
    
    public List<Cpu> DeserializeToList(object content)
    {
        return JsonConvert.DeserializeObject<List<Cpu>>(content.ToString() ?? string.Empty);
    }

    public T DeserializeToSingle<T>(object content)
    {
        throw new NotImplementedException();
    }

    public List<T> DeserializeToList<T>(object content)
    {
        throw new NotImplementedException();
    }
}