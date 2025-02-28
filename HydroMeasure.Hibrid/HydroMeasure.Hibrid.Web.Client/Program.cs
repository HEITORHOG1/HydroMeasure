using HydroMeasure.Hibrid.Shared.Services;
using HydroMeasure.Hibrid.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the HydroMeasure.Hibrid.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
// Registrar serviço de API
builder.Services.AddScoped<IApiService, ApiService>();
// Registrar serviço de Condominio
builder.Services.AddScoped<ICondominioService, CondominioService>();
// Registrar serviços adicionais
builder.Services.AddScoped<IUnidadeService, UnidadeService>();
builder.Services.AddScoped<IAlertaService, AlertaService>();
builder.Services.AddScoped<IConfiguracaoCondominioService, ConfiguracaoCondominioService>();
// Register HTTP client 
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

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