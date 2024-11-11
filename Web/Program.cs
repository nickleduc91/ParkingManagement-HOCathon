using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Add Entity Framework
builder.Services.AddDbContext<ParkingManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ParkingManagementConnection")));
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")));

builder.Services.AddIdentity<ParkingUser, IdentityRole>()
            .AddRoles<IdentityRole>()
           .AddDefaultUI()
           .AddEntityFrameworkStores<UserContext>()
                           .AddDefaultTokenProviders();

builder.Services.AddCoreServices(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Owner", "Parker" };
    foreach (var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var scopedProvider = scope.ServiceProvider;
    try
    {
        var parkingManagementContext = scopedProvider.GetRequiredService<ParkingManagementDbContext>();
        await ParkingManagementDbContextSeed.SeedAsync(parkingManagementContext, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
