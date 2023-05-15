using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Online_Pizza_Ordering_System.Repositories.Abstract;
using Online_Pizza_Ordering_System.Repositories.Implementation;
using Online_Pizza_Ordering_System.Data;
using Online_Pizza_Ordering_System.Models;
using Online_Pizza_Ordering_System.Repositories;
using Online_Pizza_Ordering_System.Repositories.Abstract;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
///////////////////////////////////////////////
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

//////////////////////////////////////////////////////
// Add services to the container.
builder.Services.AddControllersWithViews();


//////////////////////////////////////////

builder.Services.AddMvc();
builder.Services.AddTransient<IStoresRepositories, StoresRepositories>();
builder.Services.AddTransient<IPizzaRepositories, PizzaRepositories>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => ShoppingCart.GetCart(sp));





builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Online_Pizza_Ordering_System.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/LogOut";
});



/////////////////////////////////////////////////////

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(context, services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
///////////////
app.UseSession();
///////////////
app.UseAuthentication();
//////////////////
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
