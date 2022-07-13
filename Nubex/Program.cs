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
using Microsoft.OpenApi.Models;
using Nubex.Data.Helper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddControllers();
//## MudBlazor
//builder.Services.AddMudServices();
//## SyncFusion
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjU5NTM5QDMyMzAyZTMxMmUzMGpyd1p6cE9sN3JmNU1mamt4d2NDd2c3Z1hGNTA4enNnNWhtbWpWZm53dm89");
builder.Services.AddSyncfusionBlazor();
//Add AppDBContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddSwaggerGen();

var apiSettingsSection = builder.Configuration.GetSection("APISettings");
builder.Services.Configure<APISettings>(apiSettingsSection);

var apiSettings = apiSettingsSection.Get<APISettings>();
var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);



builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductPremiumRepository, ProductPremiumRepository>();
//builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(o =>
{
    o.AddPolicy("Nubex",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

        });
    o.AddPolicy("PriceApi",
        policy =>
        {
            policy.WithOrigins("https://gold-feed.com/paid",
                "https://gold-feed.com/paid/d7d6s6d66k4j4658e6d6cds638940e/xmlgold_myr.php")
            .AllowAnyMethod()
            .WithHeaders(HeaderNames.ContentType, "text/plain")
            .AllowAnyOrigin();
        });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe")["ApiKey"];
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
////    The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
app.UseSwagger();
if (!app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nubex Blazor API v1");
        c.RoutePrefix = String.Empty;
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("Nubex");
app.UseCors("PriceApi");
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