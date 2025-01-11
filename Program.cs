using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using MyMoney.Components;
using MyMoney.Data;
using Microsoft.AspNetCore.Components.Authorization;
using MyMoney.Components.Account;

var builder = WebApplication.CreateBuilder(args);

// Validação da configuração
// Configuração do Razor e Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents().AddHubOptions(options =>
    {
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
        options.HandshakeTimeout = TimeSpan.FromSeconds(30);
    });
builder.Services.AddQuickGridEntityFrameworkAdapter();

var DefaultConnection = builder.Configuration["MyMoney:DefaultConnection"];
if (string.IsNullOrEmpty(DefaultConnection))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

// Configuração do DbContext
builder.Services.AddDbContextFactory<MyMoneyContext>(options =>
    options.UseNpgsql(DefaultConnection));

// Configuração do MudBlazor
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.VisibleStateDuration = 1500;
});

// Configuração do Identity
builder.Services.AddIdentityCore<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<MyMoneyContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

// Configuração dos cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/access-denied";
    options.SlidingExpiration = true;
});

// Configuração de autenticação e estado
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// Configuração de email
builder.Services.AddSingleton<IEmailSender<IdentityUser>, IdentityNoOpEmailSender>();


// Configuração de controladores e outros serviços
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();


// Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();    
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.MapBlazorHub(config =>
{
    config.CloseOnAuthenticationExpiration = true;
});

app.UseAuthentication();
app.UseAuthorization();



app.MapAdditionalIdentityEndpoints();


app.Run();
