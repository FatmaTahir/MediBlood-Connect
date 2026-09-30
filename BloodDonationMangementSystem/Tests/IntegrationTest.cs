using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BloodDonationMangementSystem.Repositories;
using BloodDonationManagementSystem.Models;

namespace Tests
{
    public class IntegrationTest
    {
        [Fact]
        public void GetDonorById_ReturnsDonor_FromDatabase()
        {
            // Arrange
            var repo = new DonorRepository();

            // ⚠️ Use an ID that EXISTS in your AspNetUsers table
            string donorId = "01595e00-6aee-4ce3-a7a2-02b99e284e6a";

           
            var donor = repo.GetDonorById(donorId);

           
            Assert.NotNull(donor);
            Assert.Equal(donorId, donor.Id);
        }
        [Fact]
        public void UpdateDonor_UpdatesData_InDatabase()
        {
            var repo = new DonorRepository();

            var donor = new User
            {
                Id = "01595e00-6aee-4ce3-a7a2-02b99e284e6a",   
                FullName = "Tahir",
                Age = 25,
                Contact = "03001234567",
                BloodGroup = "B+",
                CNIC = "12345-6789012-3",
                Availability = true
            };

            // Act
            repo.UpdateDonor(donor);

            // Fetch again
            var updatedDonor = repo.GetDonorById(donor.Id);

            // Assert
            Assert.Equal("Updated Test Name", updatedDonor.FullName);
            Assert.Equal("B+", updatedDonor.BloodGroup);
        }

    }
}
