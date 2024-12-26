using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MyMoney.Components;
using MyMoney.Data;

var builder = WebApplication.CreateBuilder(args);
var DefaultConnection = builder.Configuration["MyMoney:DefaultConnection"];

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<MyMoneyContext>(options =>
    options.UseNpgsql(DefaultConnection ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();



builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddHttpClient();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7152/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
