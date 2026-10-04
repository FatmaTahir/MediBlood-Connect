using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BloodDonationManagementSystem.Models;

namespace BloodDonationManagementSystem.Data;

public class BloodDonationManagementSystemContext : IdentityDbContext<User>
{
    public BloodDonationManagementSystemContext(DbContextOptions<BloodDonationManagementSystemContext> options)
        : base(options)
    {
    }

    // Register custom model tables for Entity Framework Core
    public DbSet<DonationRequest> DonationRequests { get; set; }
    public DbSet<BloodRequest> BloodRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Map tables explicitly to singular names to match Dapper SQL queries
        builder.Entity<DonationRequest>().ToTable("DonationRequest");
        builder.Entity<BloodRequest>().ToTable("BloodRequest");
    }
}