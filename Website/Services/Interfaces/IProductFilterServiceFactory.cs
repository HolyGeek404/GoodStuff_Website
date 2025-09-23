using GoodStuff_DomainModels.Models.Enums;

namespace Website.Services.Interfaces;

public interface IProductFilterServiceFactory
{
    IProductFilterService Get(ProductCategories type);
}