using System.ComponentModel.DataAnnotations;

namespace BloodDonationManagementSystem.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; } // Primary Key (Auto-incremented)

        // Foreign key to ASP.NET Core Identity User
        public string UserId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string BloodGroup { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string? HospitalName { get; set; }
    }
}