using Microsoft.AspNetCore.Identity;
namespace BloodDonationManagementSystem.Models
{
    public class Donor : IdentityUser
    {
        public int DonorId { get; set; } //primary key //auto-gen
        public string FullName { get; set; }
        public int Age { get; set; }
        public string CNIC { get; set; }
        public string Contact { get; set; }
        public string BloodGroup { get; set; }
       
        public string RegistrationStatus { get; set; }
        public bool Availability { get; set; }
        //public DateOnly LastDonationDate { get; set; }
      
    }
}
