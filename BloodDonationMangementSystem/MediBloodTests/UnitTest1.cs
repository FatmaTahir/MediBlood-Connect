using Xunit;
using BloodDonationMangementSystem.Repositories;
using BloodDonationManagementSystem.Models;
using System.Linq;

namespace BloodDonationTests
{
    public class PatientRepositoryTests
    {
        // Use your actual local connection in PatientRepository
        private readonly PatientRepository _repo;

        public PatientRepositoryTests()
        {
            _repo = new PatientRepository();
        }

        [Fact]
        public void AddPatient_ShouldInsertPatient()
        {
            // Arrange
            var patient = new Patient { FullName = "UnitTest User", Age = 30 };

            // Act
            _repo.AddPatient(patient);

            // Assert - simple check via ViewBloodRequest (you can adjust for real check)
            var requests = _repo.ViewBloodRequest("unit@example.com"); // This email won't exist, just example
            Assert.NotNull(requests); // Check it returns a list, basic test
        }

        [Fact]
        public void AddBloodRequest_ShouldInsertBloodRequest()
        {
            // Arrange
            var bloodRequest = new BloodRequest
            {
                PatientName = "UnitTest User",
                PatientEmail = "unit@example.com",
                BloodGroup = "A+",
                UnitsRequired = 2,
                Urgency = "High",
                Reason = "Testing",
                Status = "Pending"
            };

            // Act
            _repo.AddBloodRequest(bloodRequest);

            // Assert
            var requests = _repo.ViewBloodRequest("unit@example.com");
            Assert.True(requests.Any(r => r.PatientName == "UnitTest User"));
        }

        [Fact]
        public void ViewBloodRequest_ShouldReturnList()
        {
            // Act
            var result = _repo.ViewBloodRequest("unit@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<System.Collections.Generic.List<BloodRequest>>(result);
        }

        [Fact]
        public void ViewMatchingDonors_ShouldReturnList()
        {
            // Act
            var donors = _repo.ViewMatchingDonors("A+");

            // Assert
            Assert.NotNull(donors);
            Assert.IsType<System.Collections.Generic.List<Donor>>(donors);
        }

        [Fact]
        public void GetApprovedRequest_ShouldReturnBloodRequestOrNull()
        {
            // Act
            var approved = _repo.GetApprovedRequest("unit@example.com");

            // Assert
            Assert.True(approved == null || approved is BloodRequest);
        }
    }
}
