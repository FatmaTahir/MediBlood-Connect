namespace BloodDonationManagementSystem.Models
{
    public class BloodRequest
    {
        public int BloodRequestId { get; set; } //auto-gen //primary-key
        public string PatientName{ get; set; } //get for patients
        public string PatientEmail { get; set; } //for session
        public string Reason {  get; set; }
        public string BloodGroup {  get; set; }
        public int UnitsRequired {  get; set; }
        public string Urgency {  get; set; }
        public string Status {  get; set; }  //managed by admin
        //public DateOnly RequestDate { get; set; } //datetime.now

    }
}
