using System.Net.Http.Headers;
using GoodStuff_Blazor.Api;
using GoodStuff_Blazor.Services;
using GoodStuff_Blazor.Services.Interfaces;

namespace GoodStuff_Blazor;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddHttpClient<GoodStuffApiClient>( client =>
        {
            var goodStuffApiUrl = GetApiUrl(configuration);
          
            client.BaseAddress = new Uri(goodStuffApiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddTransient<IRequestMessageBuilder, RequestMessageBuilder>();

        return services;
    }

    private static string GetApiUrl(IConfiguration configuration)
    {
        var env = Environment.GetEnvironmentVariable("IsDocker");
        return env.Equals("false") ? configuration.GetSection("ApiUrls")["GoodStuffApi"] : "http://webapi:8080/";
    }
}