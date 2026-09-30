using BloodDonationManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace BloodDonationManagementSystem.Interfaces
{
    public interface IAdminRepository
    {
        public List<DonationRequest> ViewDonationRequests(string bloodGroup,string status);
        public  List<User> ViewDonors(string bloodGroup = "", string availability = "");
        public List<BloodRequest> ViewBloodRequests(string bloodGroup, string status);
        public int GetTotalDonationRequests();
        public int GetTotalBloodRequests();
        public void UpdateDonationRequestStatus(int requestId);
        public string GetDonorIdByRequest(int requestId);
        public void ApproveBloodRequest(int requestId);
        public void RejectBloodRequest(int requestId);
        public string GetPatientIdByBloodRequest(int requestId);
        User GetDonorById(string id);
        DonationRequest GetDonationById(int requestid);
        public void ApproveDonation(int requestId);
        public void RejectDonation(int requestId);

    }
}
