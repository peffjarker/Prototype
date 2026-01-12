using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Prototype.Components;
using Prototype.Components.Layout.Navigation.Sidebar;
using Prototype.Components.Services;
using Prototype.Components.Services.Reports;
using Prototype.Components.Services.Search;
using Prototype.Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add authentication
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.SaveTokens = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("offline_access");
});

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    // Require authenticated users by default for all pages
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy(AppPolicies.CustomerService, policy =>
        policy.RequireClaim("groups", "84b76a76-52e2-4310-9767-3f40b7515ae6"));
    options.AddPolicy(AppPolicies.Admin, policy =>
        policy.RequireClaim("groups", "34292b7e-74b4-4c44-b0c8-deb3859f44b2"));
    options.AddPolicy(AppPolicies.Purchasing, policy =>
        policy.RequireClaim("groups", "5076c722-89d1-4afb-bfa0-e261a37728c2"));
    options.AddPolicy(AppPolicies.TechCredit, policy =>
        policy.RequireClaim("groups", "2780fc34-4d65-4445-b4b5-1fe59780f5a6"));
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddTelerikBlazor();
builder.Services.AddScoped<SidePanelService>();
builder.Services.AddScoped<ISidebarState, SidebarState>();
builder.Services.AddScoped<IItemsOnOrderPdfService, ItemsOnOrderPdfService>();
builder.Services.AddScoped<IUrlState, UrlState>();
builder.Services.AddSingleton<IFranchiseService, FranchiseService>();
builder.Services.AddSearchServices();
builder.Services.AddControllers();
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
