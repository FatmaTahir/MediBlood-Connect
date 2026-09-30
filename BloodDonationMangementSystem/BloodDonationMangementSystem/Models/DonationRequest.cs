namespace BloodDonationManagementSystem.Models
{
    public class DonationRequest
    {
        public int DonationId { get; set; }
        public string DonorId { get; set; }
        public string FullName { get; set; }
        public string BloodGroup { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }  //add in db pending

    }
}
