using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarRepair.MVC.Data;
using CarRepair.MVC.Interfaces;
using CarRepair.MVC.Repositories;
using CarRepair.MVC.Settings;
using CarRepair.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Database Retry service
builder.Services.Configure<DatabaseReconnectSettings>(builder.Configuration.GetSection("DatabaseReconnectSettings"));

// repositories
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddSingleton<IDatabaseRetryService, DatabaseRetryService>();

// Caching
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Create Roles
using( var scope = app.Services.CreateScope())
{
    await RolesService.CreateRoles(scope.ServiceProvider);

    await AdminService.CreateAdminEmail(scope.ServiceProvider);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
