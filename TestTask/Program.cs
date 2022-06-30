using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TestTask.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("FirstConnection");
builder.Services.AddDbContext<AppFirstDBContent>(options => options.UseSqlServer(connection));
string connection2 = builder.Configuration.GetConnectionString("SecondConnection");
builder.Services.AddDbContext<AppSecondDBContent>(options => options.UseSqlServer(connection2));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Photos}/{action=Index}/{id?}");

app.Run();
