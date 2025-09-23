using GoodStuff_DomainModels.Models.Enums;

namespace Website.Services.Interfaces;

public interface IComponentResolver
{
    Type Resolve(ProductCategories key);
}