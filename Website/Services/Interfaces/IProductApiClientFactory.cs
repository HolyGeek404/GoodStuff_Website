using GoodStuff_DomainModels.Models.Enums;

namespace Website.Services.Interfaces;

public interface IProductApiClientFactory
{
    IProductApiClient Get(ProductCategories type);
}