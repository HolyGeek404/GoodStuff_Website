using GoodStuff_DomainModels.Models.Enums;

namespace GoodStuff.Core.Services.Interfaces;

public interface IProductServiceFactory
{
    IProductService Get(ProductCategories type);
}