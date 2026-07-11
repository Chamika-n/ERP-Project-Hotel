using GrandHotel.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();    

builder.Services.AddHttpContextAccessor();    
builder.Services.AddSession();

// මල්ලි, මෙන්න මෙතන අපි ඩේටාබේස් එක සම්බන්ධ කරා ප්‍රොජෙක්ට් එකට!
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

// මෙන්න මෙතන අපි Default Controller එක Billing විදිහට සෙට් කරා
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Billing}/{action=Index}/{id?}");

app.Run();