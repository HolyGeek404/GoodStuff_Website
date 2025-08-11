using GoodStuff_DomainModels.Models.Products;
using Newtonsoft.Json;
using Website.Services.Interfaces;

namespace Website.Services.Product;

public class CpuProductDeserializer : IProductDeserializer<Cpu>
{
    public Cpu DeserializeToSingle(object content)
    {
        return JsonConvert.DeserializeObject<Cpu>(content.ToString() ?? string.Empty);
    }
    
    public List<Cpu> DeserializeToList(object content)
    {
        return JsonConvert.DeserializeObject<List<Cpu>>(content.ToString() ?? string.Empty);
    }
}