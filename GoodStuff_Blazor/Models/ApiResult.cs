using System.Net;

namespace GoodStuff_Blazor.Models;

public class ApiResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}