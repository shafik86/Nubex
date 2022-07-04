using Microsoft.EntityFrameworkCore;
using Nubex_Business.Repository;
using Nubex_Business.Repository.IRepository;
using Nubex_DataAccess.Data;
using Microsoft.Net.Http.Headers;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductPremiumRepository, ProductPremiumRepository>();
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("Nubex");
app.UseCors("PriceApi");
app.UseAuthorization();

app.MapControllers();

app.Run();
