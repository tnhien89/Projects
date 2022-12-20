using FastDeploy.Business;
using FastDeploy.Business.Interfaces;
using FastDeploy.DataAccess;
using FastDeploy.DataAccess.Interfaces;
using FastDeploy.Services;
using FastDeploy.Services.Interfaces;
using FastDeploy.Utilities;
using FastDeploy.Utilities.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDataProvider, DataProvider>();
builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();

builder.Services.AddScoped<IUserBusiness, UserBusiness>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(option => {
    option.Cookie.Name = ".FastDeploy.Session";
    option.IdleTimeout = TimeSpan.FromMinutes(2);
    option.Cookie.IsEssential = true;
});

var app = builder.Build();

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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{action}",
    defaults: new { controller = "Home", action = "Index" });

app.Run();
