using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleECommerce.Controllers.Filter;
using SimpleECommerce.InfraStructure;
using SimpleECommerce.InfraStructure.Catalog;
using SimpleECommerce.InfraStructure.Image;
using SimpleECommerce.InfraStructure.Logging;
using SimpleECommerce.InfraStructure.Logging.Impl;
using SimpleECommerce.Models.Context;
using SimpleECommerce.Models.User.Authorization;
using SimpleECommerce.Service.Catalog;
using SimpleECommerce.Service.Image;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ECommerceDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("ApplicationDbContext")
    )
);

// DI対称とするクラスの登録
builder.Services.AddScoped<IProductRepository, ProductRepositoryImpl>();
builder.Services.AddScoped<IProductService, ProductServiceImpl>();
builder.Services.AddScoped<IImageStorage, ImageStorageImpl>();
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(ConsoleLogger<>));
builder.Services.AddScoped<ActionFilter>();

builder.Services.AddControllersWithViews(options => {
    options.Filters.Add<ActionFilter>();
    // 更新系リクエストに対するCSRF対策有効化
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});


builder.Services.AddAuthentication("AuthCookie").AddCookie(
    "AuthCookie", options => 
    {
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
        options.AccessDeniedPath = "/auth/denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(3);
    }
);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ViewProduct", policy =>
    {
        policy.RequireClaim("Permission", "ViewProduct");
    })
    .AddPolicy("RegisterNewProduct", policy =>
    {
        policy.RequireClaim("Permission", "RegisterNewProduct");
    })
    .AddPolicy("UpdateProduct", policy =>
    {
        policy.RequireClaim("Permission", "UpdateProduct");
    })
    .AddPolicy("DeleteProduct", policy =>
    {
        policy.RequireClaim("Permission", "DeleteProduct");
    })
    .AddPolicy("NewOrder", policy =>
    {
        policy.RequireClaim("Permission", "NewOrder");
    })
    .AddPolicy("UpdateOrder", policy =>
    {
        policy.RequireClaim("Permission", "UpdateOrder");
    })
    .AddPolicy("CancelOrder", policy =>
    {
        policy.RequireClaim("Permission", "CancelOrder");
    });

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
