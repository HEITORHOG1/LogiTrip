using LogiTrip.Hybrid.Shared.Endpoints;
using LogiTrip.Hybrid.Shared.Services;
using LogiTrip.Hybrid.Shared.Services.Interfaces;
using LogiTrip.Hybrid.Web.Components;
using LogiTrip.Hybrid.Web.Services;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
// Adicione o CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7251", "http://localhost:5137")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddScoped<HttpClient>(sp =>
{
    var client = new HttpClient
    {
        BaseAddress = new Uri(ApiConstants.BaseUrl)
    };
    return client;
});
// Configuração de serviços
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Serviços do sistema
builder.Services.AddHttpClient();
builder.Services.AddAntiforgery();

// Serviços de autenticação e estado
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddMemoryCache();

// Serviços de negócio
builder.Services.AddScoped<IEstabelecimentoService, EstabelecimentoService>();
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IOpcaoProdutoService, OpcaoProdutoService>();

// Configuração do HttpClient
builder.Services.AddScoped<HttpClient>();
builder.Services.AddHttpContextAccessor();

// Serviços do MudBlazor
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

// Configuração de logging
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddConsole();

if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddDebug();
}

var app = builder.Build();

// Configuração do pipeline de requisição
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
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// Configuração dos endpoints
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(LogiTrip.Hybrid.Shared._Imports).Assembly);

app.Run();