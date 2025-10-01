using Autofac.Extensions.DependencyInjection;
using Azure.Identity;
using GoodStuff.Website.Presentation;
using GoodStuff.Website.Presentation.Components;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.UseSerilog((context, loggerConfiguration) => { loggerConfiguration.WriteTo.Console(); });

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddControllers();
builder.Services.AddServices(builder);
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();

var azureAd = builder.Configuration.GetSection("AzureAd");
builder.Configuration.AddAzureKeyVault(new Uri(azureAd["KvUrl"]), new DefaultAzureCredential());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(azureAd);
builder.Services.AddAuthorization();
builder.Services.AddHttpClient();
builder.Services.AddHttpGoodStuffProductApiClient(builder.Configuration);
builder.Services.AddHttpGoodStuffUserApiClient(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();