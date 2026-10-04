using BloodDonationManagementSystem.Models;

namespace BloodDonationManagementSystem.Interfaces
{
    public interface IPatientRepository
    {
        void AddPatient(Patient p);
        void AddBloodRequest(BloodRequest br);
        //int GetPatientIdByUserId(string userId);
        List<BloodRequest> ViewBloodRequest(string email);
      //  List<Donor> ViewMatchingDonors(Patient p);
        List<User> ViewMatchingDonors(string bloodGroup);
        BloodRequest GetApprovedRequest(string email);

    }
}
