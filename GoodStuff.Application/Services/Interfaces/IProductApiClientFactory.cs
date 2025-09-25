using GoodStuff_DomainModels.Models.Enums;

namespace GoodStuff.Application.Services.Interfaces;

public interface IProductApiClientFactory
{
    IProductApiClient Get(ProductCategories type);
}