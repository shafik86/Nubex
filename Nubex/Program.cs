using Microsoft.EntityFrameworkCore;
using Nubex.Data;
using Nubex_DataAccess.Data;
using Nubex.Service;
using Nubex.Service.IService;
using Nubex_Business.Repository;
using Nubex_Business.Repository.IRepository;
using Syncfusion.Blazor;
using MudBlazor;
using MudBlazor.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
//## MudBlazor
//builder.Services.AddMudServices();
//## SyncFusion
builder.Services.AddControllers();
builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });
//Add AppDBContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductPremiumRepository, ProductPremiumRepository>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddControllers();
var app = builder.Build();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjU5NTM5QDMyMzAyZTMxMmUzMGpyd1p6cE9sN3JmNU1mamt4d2NDd2c3Z1hGNTA4enNnNWhtbWpWZm53dm89");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

SeedDatabase();
app.UseAuthentication();;
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}