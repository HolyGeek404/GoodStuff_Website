using Website;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Azure.Identity;
using Website.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddServices(builder.Configuration);
builder.Logging.AddLoggingConfig();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();

var azureAd = builder.Configuration.GetSection("AzureAd");
builder.Configuration.AddAzureKeyVault(new Uri(azureAd["KvUrl"]!), new DefaultAzureCredential());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(azureAd);
builder.Services.AddAuthorization();
builder.Services.AddHttpGoodStuffProductApiClient(builder.Configuration);
builder.Services.AddHttpGoodStuffUserApiClient(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
