using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Config;
using SimpleECommerce.Controllers.Auth.Ext;
using SimpleECommerce.Controllers.Filter;
using SimpleECommerce.Domain.Catalog.Factory;
using SimpleECommerce.InfraStructure;
using SimpleECommerce.InfraStructure.Catalog;
using SimpleECommerce.InfraStructure.Image;
using SimpleECommerce.InfraStructure.Logging;
using SimpleECommerce.InfraStructure.Logging.Impl;
using SimpleECommerce.InfraStructure.Purchase;
using SimpleECommerce.InfraStructure.Purchase.Shopping;
using SimpleECommerce.InfraStructure.User;
using SimpleECommerce.Middleware;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Models.User.Authorization;
using SimpleECommerce.Service.Catalog;
using SimpleECommerce.Service.Image;
using SimpleECommerce.Service.Purchase;
using SimpleECommerce.Service.Purchase.Shopping;
using SimpleECommerce.Service.User;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ECommerceDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("ApplicationDbContext")
    )
);

// Redis接続
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
    configuration.AbortOnConnectFail = false;
    return ConnectionMultiplexer.Connect(configuration);
});

// DI対称とするクラスの登録
builder.Services.AddScoped<IProductRepository, ProductRepositoryImpl>();
builder.Services.AddScoped<IProductService, ProductServiceImpl>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IPurchasePointRepository, PurchasePointRepositoryImpl>();
builder.Services.AddScoped<IPurchasePointService, PurchasePointServiceImpl>();
builder.Services.AddScoped<ICartRepository, CartRepositoryImpl>();
builder.Services.AddScoped<IImageStorage, ImageStorageImpl>();
builder.Services.AddSingleton<IProductIdGenerator, SimpleProductIdGenerator>();
builder.Services.AddSingleton<ProductFactory>();
builder.Services.AddSingleton(typeof(IAppLogger<>), typeof(ConsoleLogger<>));
builder.Services.AddSingleton<ActionFilter>();

builder.Services.AddControllersWithViews(options => {
    options.Filters.Add<ActionFilter>();
    // 更新系リクエストに対するCSRF対策有効化
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

builder.Services.AddCookeiAythentication();
builder.Services.AddPermissionPolicies();

// ポイント付与関連の設定情報の読み込み
builder.Services.AddOptions<PointSettings>().Bind(builder.Configuration.GetSection(nameof(PointSettings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// DBの初期データ投入
using (IServiceScope scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
    DbInitializer.Seed(db);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseGlobalExceptionHandlerMiddleware();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
