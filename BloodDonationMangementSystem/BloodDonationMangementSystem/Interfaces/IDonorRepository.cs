using BloodDonationManagementSystem.Models;

namespace BloodDonationManagementSystem.Interfaces
{
    public interface IDonorRepository
    {

        User GetDonorById(string id);
        void UpdateDonor(User d);
        void AddDonationRequest(DonationRequest dr,string id);
        public DonationRequest GetDonationRequest(string id);
    }
}
