using HydroMeasure.Hibrid.Shared.Services;
using HydroMeasure.Hibrid.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configurar logging
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// Add device-specific services used by the HydroMeasure.Hibrid.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

// Register HTTP client with base address
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Registrar serviços da aplicação
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<ICondominioService, CondominioService>();
builder.Services.AddScoped<IUnidadeService, UnidadeService>();
builder.Services.AddScoped<IAlertaService, AlertaService>();
builder.Services.AddScoped<IConfiguracaoCondominioService, ConfiguracaoCondominioService>();
builder.Services.AddScoped<IHidrometroService, HidrometroService>();
builder.Services.AddScoped<ILeituraService, LeituraService>();
builder.Services.AddScoped<IConfiguracaoSistemaService, ConfiguracaoSistemaService>();

// Register MudBlazor services
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
});

await builder.Build().RunAsync();