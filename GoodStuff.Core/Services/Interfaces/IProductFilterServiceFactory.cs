using GoodStuff_DomainModels.Models.Enums;

namespace GoodStuff.Core.Services.Interfaces;

public interface IProductFilterServiceFactory
{
    IProductFilterService Get(ProductCategories type);
}