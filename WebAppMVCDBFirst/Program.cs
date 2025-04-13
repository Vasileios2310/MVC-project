using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebAppMVCDBFirst.Models;
using WebAppMVCDBFirst.Repositories;
using WebAppMVCDBFirst.Services;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");

// AddDbContext is scoped - per request a new instance  of dbcontext is created
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connString));

builder.Host.UseSerilog((context, config) => {
                                            config.ReadFrom.Configuration(context.Configuration);
                                                });

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IApplicationService, ApplicationService>();

builder.Services.AddRepositories();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                                                .AddCookie(option =>
                                                                {
                                                                    option.LoginPath = ("/User/Login");
                                                                    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=User}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();