using Prototype.Components;
using Prototype.Components.Layout.Navigation.Sidebar;
using Prototype.Components.Pages.POXfer.ItemsOnOrder;
using Prototype.Components.Services.Reports;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddTelerikBlazor();
builder.Services.AddScoped<ISidebarState, SidebarState>();
builder.Services.AddScoped<IItemsOnOrderPdfService, ItemsOnOrderPdfService>();
builder.Services.AddScoped<IUrlState, UrlState>();
builder.Services.AddSingleton<IFranchiseService, FranchiseService>();
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
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
