namespace Website.Services.Interfaces;

public interface IProductDeserializer
{
    public T DeserializeToSingle<T>(object content);
    public List<T> DeserializeToList<T>(object content);
}