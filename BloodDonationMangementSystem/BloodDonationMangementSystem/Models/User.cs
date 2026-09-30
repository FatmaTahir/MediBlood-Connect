using Microsoft.AspNetCore.Identity;


namespace BloodDonationManagementSystem.Models
{
    public class User : IdentityUser
    { 
        //donor-patient properties
        public string? FullName { get; set; }
        public int Age { get; set; }
        //donor-only
        public string? CNIC { get; set; }
        public string? Contact { get; set; }
        public string? BloodGroup { get; set; }
        public bool Availability { get; set; }
        
    }
}
