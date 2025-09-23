using GoodStuff_DomainModels.Models.Enums;

namespace Website.Services.Interfaces;

public interface IProductServiceFactory
{
    IProductService Get(ProductCategories type);
}