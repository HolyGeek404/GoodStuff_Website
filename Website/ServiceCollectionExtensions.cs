using System.Net.Http.Headers;
using Autofac;
using GoodStuff_DomainModels.Models.Products;
using Website.Api;
using Website.Factories;
using Website.Services.Filters;
using Website.Services.Interfaces;
using Website.Services.Other;
using Website.Services.Product;
using IProductFilterService = Website.Services.Interfaces.IProductFilterService;

namespace Website;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<IRequestMessageBuilder, RequestMessageBuilder>();
        services.AddTransient<ITokenProvider, TokenProvider>();
        services.AddTransient<IProductApiClientFactory, ProductApiClientFactory>();
        services.AddTransient<IProductServiceFactory, ProductServiceFactory>();
        services.AddTransient<IProductFilterServiceFactory, ProductFilterServiceFactory>();
        services.AddScoped<IUserSessionService, UserSessionService>();
        services.AddSingleton<IComponentResolver, ComponentResolver>();

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<ProductApiClient<GpuModel>>().Keyed<IProductApiClient>("GPU");
            containerBuilder.RegisterType<ProductApiClient<CpuModel>>().Keyed<IProductApiClient>("CPU");
            containerBuilder.RegisterType<ProductApiClient<CoolerModel>>().Keyed<IProductApiClient>("COOLER");

            containerBuilder.RegisterType<ProductService<CpuModel>>()
                .WithParameter("category", "CPU")
                .Keyed<IProductService>("CPU");

            containerBuilder.RegisterType<ProductService<GpuModel>>()
                .WithParameter("category", "GPU")
                .Keyed<IProductService>("GPU");

            containerBuilder.RegisterType<ProductService<CoolerModel>>()
                .WithParameter("category", "COOLER")
                .Keyed<IProductService>("COOLER");
            
            containerBuilder.RegisterType<CpuFilterService>().Keyed<IProductFilterService>("CPU");
            containerBuilder.RegisterType<GpuFilterService>().Keyed<IProductFilterService>("GPU");
            containerBuilder.RegisterType<CoolerFilterService>().Keyed<IProductFilterService>("COOLER");
        });

        return services;
    }

    public static IServiceCollection AddHttpGoodStuffProductApiClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient("ProductClient", client =>
        {
            var isDocker = Environment.GetEnvironmentVariable("IsDocker")!;
            string apiUrl;
            if (!string.IsNullOrEmpty(isDocker) && isDocker.Equals("true", StringComparison.OrdinalIgnoreCase))
                apiUrl = configuration.GetSection("DockerUrls")["ProductApiBaseUrl"]!;
            else
                apiUrl = configuration.GetSection("GoodStuffProductApi")["Url"]!;

            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }

    public static IServiceCollection AddHttpGoodStuffUserApiClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<UserApiClient>(client =>
        {
            var isDocker = Environment.GetEnvironmentVariable("IsDocker")!;
            string apiUrl;
            if (!string.IsNullOrEmpty(isDocker) && isDocker.Equals("true", StringComparison.OrdinalIgnoreCase))
                apiUrl = configuration.GetSection("DockerUrls")["UserApiBaseUrl"]!;
            else
                apiUrl = configuration.GetSection("GoodStuffUserApi")["Url"]!;

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