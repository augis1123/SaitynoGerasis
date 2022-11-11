using Microsoft.AspNetCore.Identity;
using SaitynoGerasis.Data;
using SaitynoGerasis.Data.Repositories;
using SaitynoGerasis.Data.Entities;
using WebApplication1.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using MySql.EntityFrameworkCore.Extensions;
using SaitynoGerasis;
using SaitynoGerasis.Auth;
using Microsoft.AspNetCore.Authorization;
using SaitynoGerasis.Auth.Model;
using System.IdentityModel.Tokens.Jwt;



//using WebApplication1.Data.Repositories;

// Add services to the container.
//microsoft.entityFrameworkCore.SQLserver
//dotnet tool install --global dotnet-ef
var builder = WebApplication.CreateBuilder(args);
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddControllers();
builder.Services.AddIdentity<naudotojas, IdentityRole>().AddEntityFrameworkStores<ShopDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(configureOptions: option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters.ValidAudience = builder.Configuration["JWT:ValidAudience"];
    option.TokenValidationParameters.ValidIssuer = builder.Configuration["JWT:ValidIssuer"];
    option.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));
});
builder.Services.AddDbContext<ShopDbContext>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<ISellerRepository, SellerRepository>();
builder.Services.AddTransient<IBillRepository, BillRepository>();
builder.Services.AddTransient<ISoldProductRepository, SoldProductRepository>();

builder.Services.AddTransient<IJwtTokenService1, JwtTokenService>();
builder.Services.AddScoped<AuthDbSeeder>();
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy(PolicyNames.ResourceOwner, policy => policy.Requirements.Add(new ResourceOwnerRequirement())); 
}
    );
builder.Services.AddSingleton<IAuthorizationHandler, ResourceOwnerAuthorizationHandler>();
var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
var dbseeder = app.Services.CreateScope().ServiceProvider.GetRequiredService<AuthDbSeeder>();
await dbseeder.SeedAsync();

app.Run();
