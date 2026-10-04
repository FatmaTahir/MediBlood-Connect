using System.ComponentModel.DataAnnotations;

namespace BloodDonationManagementSystem.Models
{
    public class Donor
    {
        [Key]
        public int DonorId { get; set; } // Primary Key (Auto-incremented int)

        public string UserId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string CNIC { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string BloodGroup { get; set; } = string.Empty;
        public string RegistrationStatus { get; set; } = "Pending";
        public bool Availability { get; set; }
    }
}