using GoodStuff_DomainModels.Models.Enums;

namespace GoodStuff.Application.Services.Interfaces;

public interface IProductFilterServiceFactory
{
    IProductFilterService Get(ProductCategories type);
}