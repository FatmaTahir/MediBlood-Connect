using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationManagementSystem.Models
{
    public class DonationRequest
    {
        [Key]
        public int DonationId { get; set; }

        public string UserId { get; set; } = string.Empty; // Foreign key to AspNetUsers
        public string DonorId { get; set; } = string.Empty; // Kept for repository compatibility
        public string FullName { get; set; } = string.Empty;
        public string BloodGroup { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
﻿
