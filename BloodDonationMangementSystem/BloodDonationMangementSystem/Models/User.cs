using Microsoft.AspNetCore.Identity;

namespace BloodDonationManagementSystem.Models
{
    public class User : IdentityUser
    {
        // Shared properties
        public string? FullName { get; set; }
        public int Age { get; set; }

        // Donor-specific properties
        public string? CNIC { get; set; }
        public string? Contact { get; set; }
        public string? BloodGroup { get; set; }
        public bool Availability { get; set; }
        public string? RegistrationStatus { get; set; } = "Pending";
    }
}