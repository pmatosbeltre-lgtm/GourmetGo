using GourmetGo.Web.Components;
using GourmetGo.Web.Handlers;
using GourmetGo.Web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var apiBaseUrl = new Uri("http://localhost:5043/");


builder.Services.AddHttpClient<RestauranteApiService>(client => client.BaseAddress = apiBaseUrl);
builder.Services.AddHttpClient<UsuarioApiService>(client => client.BaseAddress = apiBaseUrl);
builder.Services.AddHttpClient<PlatoApiService>(client => client.BaseAddress = apiBaseUrl);
builder.Services.AddHttpClient<ReservaApiService>(client => client.BaseAddress = apiBaseUrl);
builder.Services.AddHttpClient<MenuApiService>(client => client.BaseAddress = apiBaseUrl);
builder.Services.AddHttpClient<PagoApiService>(client => client.BaseAddress = apiBaseUrl);



builder.Services.AddAuthentication(options =>
{
    
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/login"; 
    options.AccessDeniedPath = "/acceso-denegado";
});

builder.Services.AddAuthorization();


builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();