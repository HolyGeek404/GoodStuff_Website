using GoodStuff_DomainModels.Models.Enums;

namespace GoodStuff.Application.Services.Interfaces;

public interface IProductServiceFactory
{
    IProductService Get(ProductCategories type);
}