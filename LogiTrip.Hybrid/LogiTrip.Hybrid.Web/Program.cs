using LogiTrip.Hybrid.Shared.Services;
using LogiTrip.Hybrid.Shared.Services.Interfaces;
using LogiTrip.Hybrid.Web.Components;
using LogiTrip.Hybrid.Web.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura��o de servi�os
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Servi�os do sistema
builder.Services.AddHttpClient();
builder.Services.AddAntiforgery();

// Servi�os de autentica��o e estado
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddMemoryCache();

// Servi�os de neg�cio
builder.Services.AddScoped<IEstabelecimentoService, EstabelecimentoService>();
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IOpcaoProdutoService, OpcaoProdutoService>();

// Configura��o do HttpClient
builder.Services.AddScoped<HttpClient>();
builder.Services.AddHttpContextAccessor();

// Servi�os do MudBlazor
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.TopRight;
    config.SnackbarConfiguration.PreventDuplicates = true;
    config.SnackbarConfiguration.NewestOnTop = true;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 5000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
});

// Configura��o de logging
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddConsole();

if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddDebug();
}

var app = builder.Build();

// Configura��o do pipeline de requisi��o
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// Configura��o dos endpoints
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(LogiTrip.Hybrid.Shared._Imports).Assembly);

app.Run();