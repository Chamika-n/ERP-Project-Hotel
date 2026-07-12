using GrandHotel.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();    

builder.Services.AddHttpContextAccessor();    
builder.Services.AddSession();

// Configure the database context with the connection string from configuration
builder.Services.AddDbContext<GrandHotelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

// Set default route to Billing controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Billing}/{action=Index}/{id?}");

app.Run();
