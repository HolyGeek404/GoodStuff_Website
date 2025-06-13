using System.Net.Http.Headers;
using Website.Api;
using Website.Services;
using Website.Services.Interfaces;

namespace Website;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IRequestMessageBuilder, RequestMessageBuilder>();
        services.AddTransient<ITokenProvider, TokenProvider>();

        return services;
    }

    public static IServiceCollection AddHttpGoodStuffProductApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<GoodStuffProductApiClient>(client =>
         {
             var apiUrl = configuration.GetSection("GoodStuffProductApi")["Url"]!;
             client.BaseAddress = new Uri(apiUrl);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         });

        return services;
    }
    public static IServiceCollection AddHttpGoodStuffUserApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<GoodStuffUserApiClient>(client =>
         {
             var apiUrl = configuration.GetSection("GoodStuffUserApi")["Url"]!;
             client.BaseAddress = new Uri(apiUrl);
             client.DefaultRequestHeaders.Accept.Clear();
             client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         });

        return services;
    }
}