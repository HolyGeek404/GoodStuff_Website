using System.Net.Http.Headers;
using Autofac;
using GoodStuff_DomainModels.Models.Enums;
using GoodStuff_DomainModels.Models.Products;
using GoodStuff.Website.Application.Factories;
using GoodStuff.Website.Application.Services.Filters;
using GoodStuff.Website.Application.Services.Interfaces;
using GoodStuff.Website.Application.Services.Other;
using GoodStuff.Website.Application.Services.Product;
using GoodStuff.Website.Infrastructure.Api;

namespace GoodStuff.Website.Presentation;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddTransient<IRequestMessageBuilder, RequestMessageBuilder>();
        services.AddTransient<ITokenProvider, TokenProvider>();
        services.AddSingleton<ICacheManager, CacheManager>();
        services.AddScoped<IProductApiClientFactory, ProductApiClientFactory>();
        services.AddScoped<IProductServiceFactory, ProductServiceFactory>();
        services.AddSingleton<IProductFilterServiceFactory, ProductFilterServiceFactory>();
        services.AddTransient<IUserSessionService, UserSessionService>();

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<ProductApiClient<GpuModel>>().Keyed<IProductApiClient>(ProductCategories.Gpu);
            containerBuilder.RegisterType<ProductApiClient<CpuModel>>().Keyed<IProductApiClient>(ProductCategories.Cpu);
            containerBuilder.RegisterType<ProductApiClient<CoolerModel>>().Keyed<IProductApiClient>(ProductCategories.Cooler);

            containerBuilder.RegisterType<ProductService<CpuModel>>()
                .WithParameter("category", ProductCategories.Cpu)
                .Keyed<IProductService>(ProductCategories.Cpu);

            containerBuilder.RegisterType<ProductService<GpuModel>>()
                .WithParameter("category", ProductCategories.Gpu)
                .Keyed<IProductService>(ProductCategories.Gpu);

            containerBuilder.RegisterType<ProductService<CoolerModel>>()
                .WithParameter("category", ProductCategories.Cooler)
                .Keyed<IProductService>(ProductCategories.Cooler);

            containerBuilder.RegisterType<CpuFilterService>().Keyed<IProductFilterService>(ProductCategories.Cpu);
            containerBuilder.RegisterType<GpuFilterService>().Keyed<IProductFilterService>(ProductCategories.Gpu);
            containerBuilder.RegisterType<CoolerFilterService>().Keyed<IProductFilterService>(ProductCategories.Cooler);
        });
    }

    public static void AddHttpGoodStuffProductApiClient(this IServiceCollection services,
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
    }

    public static void AddHttpGoodStuffUserApiClient(this IServiceCollection services,
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
    }

    public static void AddLoggingConfig(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
        loggingBuilder.AddDebug();
    }
}