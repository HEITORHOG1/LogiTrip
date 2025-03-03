using LogiTrip.Hybrid.Services;
using LogiTrip.Hybrid.Shared.Services;
using LogiTrip.Hybrid.Shared.Services.Interfaces;
using Microsoft.Extensions.Logging;
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