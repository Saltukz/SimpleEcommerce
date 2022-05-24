using Microsoft.EntityFrameworkCore;
using SimpleEcommerce.Business.Abstract;
using SimpleEcommerce.Business.Concrete;
using SimpleEcommerce.Data.Abstract;
using SimpleEcommerce.Data.Concrete;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IProductService, ProductManager>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//i add this middleware to handle 404 easly 
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/";
        await next();
    }
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(name: "blog",
                pattern: "{cname}",
                defaults: new { controller = "Product", action = "List" });

app.MapControllerRoute(


    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
