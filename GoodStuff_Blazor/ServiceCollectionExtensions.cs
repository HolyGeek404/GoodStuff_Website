using System.Net.Http.Headers;
using GoodStuff_Blazor.Api;

namespace GoodStuff_Blazor;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddHttpClient<GoodStuffApiClient>( client =>
        {
            var goodStuffApiUrl = configuration.GetSection("ApiUrls")["GoodStuffApi"];
          
            client.BaseAddress = new Uri(goodStuffApiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });


        return services;
    }
}