using Website.Services.Interfaces;

namespace Website.Services.Product;

public interface IProductDeserializerFactory
{
    IProductDeserializer Get(string type);
}