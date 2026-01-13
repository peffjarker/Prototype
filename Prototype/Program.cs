using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Prototype.Authorization;
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
    .AddMicrosoftIdentityWebApp(options =>
    {
        builder.Configuration.Bind("AzureAd", options);
        options.TokenValidationParameters.NameClaimType = "preferred_username";
    });

builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.SaveTokens = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("offline_access");

    // Request group claims
    options.TokenValidationParameters.RoleClaimType = "groups";

    options.Events = new OpenIdConnectEvents
    {
        OnTokenValidated = async context =>
        {
            // This will help us debug what claims are coming through
            var claims = context.Principal?.Claims;
            if (claims != null)
            {
                foreach (var claim in claims)
                {
                    System.Diagnostics.Debug.WriteLine($"Claim: {claim.Type} = {claim.Value}");
                }
            }
            await Task.CompletedTask;
        }
    };
});

// Register custom authorization handler
builder.Services.AddSingleton<IAuthorizationHandler, ShadowAwareAuthorizationHandler>();

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    // Require authenticated users by default for all pages
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    // Define policies using custom shadow-aware requirements
    options.AddPolicy(AppPolicies.CustomerService, policy =>
        policy.Requirements.Add(new ShadowAwareRequirement(AppPolicies.CustomerService)));

    options.AddPolicy(AppPolicies.Purchasing, policy =>
        policy.Requirements.Add(new ShadowAwareRequirement(AppPolicies.Purchasing)));

    options.AddPolicy(AppPolicies.TechCredit, policy =>
        policy.Requirements.Add(new ShadowAwareRequirement(AppPolicies.TechCredit)));

    options.AddPolicy(AppPolicies.Admin, policy =>
        policy.Requirements.Add(new ShadowAwareRequirement(AppPolicies.Admin)));
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<IShadowLoginService, ShadowLoginService>();
builder.Services.AddScoped<ShadowAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<ShadowAuthenticationStateProvider>());
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
