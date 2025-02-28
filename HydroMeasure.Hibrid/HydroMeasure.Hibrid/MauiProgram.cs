using HydroMeasure.Hibrid.Services;
using HydroMeasure.Hibrid.Shared.Services;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;

namespace HydroMeasure.Hibrid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("Material-Design-Icons.ttf", "Material Design Icons");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "Material Icons");
                });

            // Add device-specific services used by the HydroMeasure.Hibrid.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

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

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}