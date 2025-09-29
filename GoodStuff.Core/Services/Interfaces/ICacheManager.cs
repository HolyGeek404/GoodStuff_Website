using GoodStuff_DomainModels.Models.Enums;
using GoodStuff_DomainModels.Models.Products;

namespace GoodStuff.Core.Services.Interfaces;

public interface ICacheManager
{
    IEnumerable<TProduct> CheckProductsInCache<TProduct>(ProductCategories category) where TProduct : BaseProductModel;

    void AddProductToCache<TProduct>(IEnumerable<TProduct> product, ProductCategories category)
        where TProduct : BaseProductModel;
}