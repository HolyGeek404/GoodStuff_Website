using System.Net.Http.Headers;
using Autofac;
using GoodStuff_DomainModels.Models.Products;
using Website.Api;
using Website.Services;
using Website.Services.Factories;
using Website.Services.Filters;
using Website.Services.Interfaces;
using Website.Services.Product;

namespace Website;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<IRequestMessageBuilder, RequestMessageBuilder>();
        services.AddTransient<ITokenProvider, TokenProvider>();
        services.AddTransient<IFilterService, FilterService>();
        services.AddTransient<IProductApiClientFactory, ProductApiClientFactory>();
        services.AddTransient<IProductFilterService, ProductFilterService>();
        services.AddTransient<IGpuProductService, GpuProductService>();
        services.AddTransient<IProductDeserializerFactory, ProductDeserializerFactory>();
        services.AddScoped<IUserSessionService, UserSessionService>();

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<GpuProductDeserializer>().Keyed<IProductDeserializer>("GPU");
            containerBuilder.RegisterType<CpuProductDeserializer>().Keyed<IProductDeserializer>("CPU");
            
            containerBuilder.RegisterType<GpuProductApiClient>().Keyed<BaseProductApiClient>("GPU");
            containerBuilder.RegisterType<CpuProductApiClient>().Keyed<BaseProductApiClient>("CPU");
        });
        
        return services;
    }

    public static IServiceCollection AddHttpGoodStuffProductApiClient(this IServiceCollection services, IConfiguration configuration)
    {
            services.AddHttpClient("ProductClient",client =>
        {
            var isDocker = Environment.GetEnvironmentVariable("IsDocker")!;
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
        services.AddHttpClient<UserApiClient>(client =>
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