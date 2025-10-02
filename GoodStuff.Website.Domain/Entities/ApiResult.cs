using System.Net;

namespace GoodStuff.Website.Domain.Entities;

public class ApiResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public object? Content { get; set; }
}