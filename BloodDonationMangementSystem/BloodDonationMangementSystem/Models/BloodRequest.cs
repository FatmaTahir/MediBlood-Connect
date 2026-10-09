using System.ComponentModel.DataAnnotations;

namespace BloodDonationManagementSystem.Models
{
    public class BloodRequest
    {
        [Key]
        public int BloodRequestId { get; set; }

        public string UserId { get; set; } = string.Empty; // Foreign key to AspNetUsers
        public string PatientName { get; set; } = string.Empty;
        public string PatientEmail { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string BloodGroup { get; set; } = string.Empty;
        public int UnitsRequired { get; set; }
        public string Urgency { get; set; } = "Normal"; // e.g., Normal, High, Critical
        public string Status { get; set; } = "Pending"; // Defaults to Pending
        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
﻿
        
      

