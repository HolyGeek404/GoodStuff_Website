using GoodStuff_DomainModels.Models.Enums;

namespace GoodStuff.Core.Services.Interfaces;

public interface IProductApiClientFactory
{
    IProductApiClient Get(ProductCategories type);
}