using SaitynoGerasis.Data;
using SaitynoGerasis.Data.Repositories;
using WebApplication1.Data;
//using WebApplication1.Data.Repositories;

// Add services to the container.
//microsoft.entityFrameworkCore.SQLserver
//dotnet tool install --global dotnet-ef
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ShopDbContext>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<ISellerRepository, SellerRepository>();
builder.Services.AddTransient<IBillRepository, BillRepository>();
builder.Services.AddTransient<ISoldProductRepository, SoldProductRepository>();
var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();
