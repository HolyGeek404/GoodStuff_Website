using GoodStuff_DomainModels.Models.Enums;

namespace GoodStuff.Application.Services.Interfaces;

public interface IComponentResolver
{
    Type Resolve(ProductCategories key);
}