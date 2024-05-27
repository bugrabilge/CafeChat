using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using CafeChat.Hubs;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNToastNotifyToastr(new NToastNotify.ToastrOptions()
    {
        PositionClass = ToastPositions.BottomRight,
        TimeOut = 3000,
        ProgressBar = true,
        NewestOnTop= true,
        CloseButton= true,

    });
builder.Services.AddSignalR();

// Dals - Repositories
builder.Services.AddScoped<IUsersDal, EfUsersRepository>();
builder.Services.AddScoped<ICafeDal, EfCafeRepository>();
builder.Services.AddScoped<IUserTypeDal, EfUserTypeRepository>();

// Services  -Managers
builder.Services.AddScoped<IUsersService, UsersManager>();
builder.Services.AddScoped<ILoginService, LoginManager>();
builder.Services.AddScoped<IAdminService, AdminManager>();

builder.Services.AddDataProtection();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.AccessDeniedPath = "/Home/AccessDeniedPage";
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseNToastNotify();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// session middleware
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chatHub");
app.Run();
