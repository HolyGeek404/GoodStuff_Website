using Microsoft.AspNetCore.Components;

namespace Website.Services.Interfaces;

public interface IProductService
{
    Task<MarkupString> BuildPreview();
    // Task<object> GetProductById(string category, string id);
}