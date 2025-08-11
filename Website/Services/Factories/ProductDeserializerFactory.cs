using Autofac.Features.Indexed;
using Website.Services.Interfaces;

namespace Website.Services.Product;

public class ProductDeserializerFactory(IIndex<string, IProductDeserializer> deserializers) : IProductDeserializerFactory
{
    public IProductDeserializer Get(string type)
    {
        return deserializers.TryGetValue(type, out var deserializer) ? deserializer : null;
    }
}