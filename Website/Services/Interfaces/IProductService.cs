using GoodStuff_DomainModels.Models.Products;

namespace Website.Services.Interfaces;

public interface IProductService 
{
    Task<IEnumerable<BaseProductModel>> GetProducts();
}