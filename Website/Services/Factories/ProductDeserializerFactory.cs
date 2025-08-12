using Autofac.Features.Indexed;
using Website.Services.Interfaces;
using Website.Services.Product;

namespace Website.Services.Factories;

public class ProductDeserializerFactory(IIndex<string, IProductDeserializer> deserializers) : IProductDeserializerFactory
{
    public IProductDeserializer Get(string type)
    {
        return deserializers.TryGetValue(type, out var deserializer) ? deserializer : null;
    }
}