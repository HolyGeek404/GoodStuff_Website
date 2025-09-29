using GoodStuff_DomainModels.Models.Enums;

namespace GoodStuff.Core.Services.Interfaces;

public interface IComponentResolver
{
    Type Resolve(ProductCategories key);
}