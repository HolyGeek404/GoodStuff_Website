using System.Net.Http.Headers;
using Autofac;
using GoodStuff_DomainModels.Models.Products;
using Website.Api;
using Website.Services.Factories;
using Website.Services.FIlters;
using Website.Services.Interfaces;
using Website.Services.Other;
using Website.Services.Product;
using Website.Services.Product.ViewBuilder;

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
        services.AddTransient<IProductServiceFactory, ProductServiceFactory>();
        services.AddTransient<IViewBuilderFactory, ViewBuilderFactory>();
        services.AddScoped<IUserSessionService, UserSessionService>();

        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<GpuProductApiClient>().Keyed<BaseProductApiClient>("GPU");
            containerBuilder.RegisterType<CpuProductApiClient>().Keyed<BaseProductApiClient>("CPU");
            
            containerBuilder.RegisterType<ProductService<CpuModel>>()
                .WithParameter("category","CPU")
                .Keyed<IProductService>("CPU");
            
            containerBuilder.RegisterType<ProductService<GpuModel>>()
                .WithParameter("category","GPU")
                .Keyed<IProductService>("GPU");
            
            containerBuilder.RegisterType<ProductService<CoolerModel>>()
                .WithParameter("category","COOLER")
                .Keyed<IProductService>("COOLER");
            
            containerBuilder.RegisterType<CpuBaseViewBuilder>().Keyed<IViewBuilder>("CPU");
            containerBuilder.RegisterType<GpuBaseViewBuilder>().Keyed<IViewBuilder>("GPU");
            containerBuilder.RegisterType<CoolerBaseViewBuilder>().Keyed<IViewBuilder>("COOLER");
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