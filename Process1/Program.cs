using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Process1.Data;
using Process1.DTOs;
using Process1.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure the ECommerceContext to use the SQL Server connection
builder.Services.AddDbContext<ECommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity with the ECommerceContext
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ECommerceContext>();  // Use ECommerceContext for Identity

// Register other services
builder.Services.AddControllersWithViews();

// Add other necessary services

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
