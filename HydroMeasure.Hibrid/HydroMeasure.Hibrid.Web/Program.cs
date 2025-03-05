using HydroMeasure.Hibrid.Shared.Services;
using HydroMeasure.Hibrid.Web.Components;
using HydroMeasure.Hibrid.Web.Services;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add device-specific services used by the HydroMeasure.Hibrid.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
// Registrar serviço de cache de memória
builder.Services.AddMemoryCache();
// Registrar serviço de API
builder.Services.AddScoped<IApiService, ApiService>();
// Registrar serviço de Condominio
builder.Services.AddScoped<ICondominioService, CondominioService>();
// Registrar serviços adicionais
builder.Services.AddScoped<IUnidadeService, UnidadeService>();
// Registrar serviço de Alerta
builder.Services.AddScoped<IAlertaService, AlertaService>();
// Registrar serviço de Configuração de Condomínio
builder.Services.AddScoped<IConfiguracaoCondominioService, ConfiguracaoCondominioService>();
// Registrar serviço de Hidrômetro
builder.Services.AddScoped<IHidrometroService, HidrometroService>();
// Registrar serviço de Leitura
builder.Services.AddScoped<ILeituraService, LeituraService>();
// Registrar serviço de Configuração do Sistema
builder.Services.AddScoped<IConfiguracaoSistemaService, ConfiguracaoSistemaService>();
// Register HTTP client
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7212") // This should be your API URL
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(HydroMeasure.Hibrid.Shared._Imports).Assembly,
        typeof(HydroMeasure.Hibrid.Web.Client._Imports).Assembly);

app.Run();