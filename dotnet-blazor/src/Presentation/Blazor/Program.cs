using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Polly;
using Radzen;
using Coffee.Presentation.Blazor;
using Coffee.Presentation.Blazor.Common;
using Coffee.Presentation.Blazor.Common.Interfaces;
using Coffee.Presentation.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var apiHttpClientName = "WeatherForecastApiClient";
var timeoutPolicy = Policy.TimeoutAsync(5);
var retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt / 2)));
var policyWrap = retryPolicy.WrapAsync(timeoutPolicy);
policyWrap = retryPolicy.WrapAsync(timeoutPolicy);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddTransient<BearerTokenHandler>();
builder.Services.AddSingleton<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddHttpClient(name: apiHttpClientName, configureClient: client => client.BaseAddress = new Uri(builder.Configuration["ApiServiceUrl"] ?? ""))
    .AddHttpMessageHandler<BearerTokenHandler>()
    .AddPolicyHandler(policyWrap);
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(apiHttpClientName));
builder.Services.AddSingleton<IApiClient>(x => new ApiClient(builder.Configuration["ApiServiceUrl"] ?? "", builder.Services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>().CreateClient(apiHttpClientName)));
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);

    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://aacnauthdev.onmicrosoft.com/25bf7e0b-5e0b-4ba2-8d54-0fc2ee8b5e6d/api.fullaccess");
});
builder.Services.AddScoped<IForecastService, ForecastService>();
builder.Services.AddBlazoredModal();
builder.Services.AddScoped<DialogService>();
builder.Services.AddSingleton<ITemperatureCalculationsExternalService, TemperatureCalculationsExternalService>();
builder.Services.AddSingleton<ITemperatureCalculationsService, TemperatureCalculationsService>();
builder.Services.AddSingleton<IExceptionResultService, ExceptionResultService>();
await builder.Build().RunAsync();