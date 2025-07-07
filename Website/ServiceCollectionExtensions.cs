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
        services.AddTransient<IFilterService, FilterService>();
        services.AddScoped<IUserSessionService, UserSessionService>();

        return services;
    }

    public static IServiceCollection AddHttpGoodStuffProductApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<GoodStuffProductApiClient>(client =>
        {
            string isDocker = Environment.GetEnvironmentVariable("IsDocker")!;
            string apiUrl;
            if (!string.IsNullOrEmpty(isDocker) && isDocker.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                apiUrl = configuration.GetSection("DockerUrls")["ProductApiBaseUrl"]!;
            }
            else
            {
                apiUrl = configuration.GetSection("GoodStuffProductApi")["Url"]!;
            }

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
            string isDocker = Environment.GetEnvironmentVariable("IsDocker")!;
            string apiUrl;
            if (!string.IsNullOrEmpty(isDocker) && isDocker.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                apiUrl = configuration.GetSection("DockerUrls")["UserApiBaseUrl"]!;
            }
            else
            {
                apiUrl = configuration.GetSection("GoodStuffUserApi")["Url"]!;
            }

            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }
    public static ILoggingBuilder AddLoggingConfig(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
        loggingBuilder.AddDebug();

        return loggingBuilder;
    }
}