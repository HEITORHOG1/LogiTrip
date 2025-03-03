using LogiTrip.Hybrid.Services;
using LogiTrip.Hybrid.Shared.Endpoints;
using LogiTrip.Hybrid.Shared.Services;
using LogiTrip.Hybrid.Shared.Services.Interfaces;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;

namespace LogiTrip.Hybrid
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
                });
            builder.Services.AddScoped<HttpClient>(sp =>
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ApiConstants.BaseUrl)
                };
                return client;
            });
            // Add this after existing MudBlazor services
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 5000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
            });
            // Add device-specific services used by the MauiApp11.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IEstabelecimentoService, EstabelecimentoService>();
            builder.Services.AddScoped<IFornecedorService, FornecedorService>();
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<IProdutoService, ProdutoService>();
            builder.Services.AddScoped<IOpcaoProdutoService, OpcaoProdutoService>();
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
            builder.Services.AddMudBlazorSnackbar();
            builder.Services.AddHttpClient();
            builder.Logging.SetMinimumLevel(LogLevel.Trace);
            builder.Logging.AddConsole();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}