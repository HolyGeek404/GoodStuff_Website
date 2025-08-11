namespace Website.Services.Product;

public abstract class BaseViewBuilder<T> where T : class
{
    public abstract string BuildPreview(List<T> productList);
    public abstract string BuildFull(List<T> gpu);
}