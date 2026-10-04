using BloodDonationManagementSystem.Data;
using BloodDonationManagementSystem.Interfaces;
using BloodDonationManagementSystem.Models;
using BloodDonationMangementSystem.Areas.Identity.Data;
using BloodDonationMangementSystem.Hubs;
using BloodDonationMangementSystem.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connection String
var connectionString = builder.Configuration.GetConnectionString("BloodDonationManagementSystemContextConnection")
    ?? throw new InvalidOperationException("Connection string 'BloodDonationManagementSystemContextConnection' not found.");

// Database Context
builder.Services.AddDbContext<BloodDonationManagementSystemContext>(options =>
    options.UseSqlServer(connectionString));

// Identity Setup
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<BloodDonationManagementSystemContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

// Services & Frameworks
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Repositories Registered for Dependency Injection
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSignalR();

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PatientAccess", policy =>
    {
        policy.RequireRole("Patient");
        policy.RequireClaim("Permission", "RequestBlood");
        policy.RequireClaim("Permission", "ViewMyRequests");
        policy.RequireClaim("Permission", "ViewMatchingDonors");
    });
    options.AddPolicy("DonorAccess", policy =>
    {
        policy.RequireRole("Donor");
        policy.RequireClaim("Permission", "ViewRequests");
        policy.RequireClaim("Permission", "UpdateProfile");
        policy.RequireClaim("Permission", "RequestDonation");
    });
    options.AddPolicy("AdminAccess", policy =>
    {
        policy.RequireRole("Admin");
        policy.RequireClaim("Permission", "ManageDonationRequests");
        policy.RequireClaim("Permission", "ViewDonors");
        policy.RequireClaim("Permission", "ManageBloodRequests");
    });
});

// Session Configuration
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Seed Roles cleanly within a disposable scope
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedRoles(roleManager);
}

// Middleware Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<NotificationHub>("/notificationHub");

app.Run();