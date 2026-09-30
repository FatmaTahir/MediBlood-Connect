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
var connectionString = builder.Configuration.GetConnectionString("BloodDonationManagementSystemContextConnection") ?? throw new InvalidOperationException("Connection string 'BloodDonationManagementSystemContextConnection' not found.");

builder.Services.AddDbContext<BloodDonationManagementSystemContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<User,IdentityRole>()
                .AddEntityFrameworkStores<BloodDonationManagementSystemContext>()
                .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSignalR();
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

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
var scope = app.Services.CreateScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
await RoleSeeder.SeedRoles(roleManager);

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
