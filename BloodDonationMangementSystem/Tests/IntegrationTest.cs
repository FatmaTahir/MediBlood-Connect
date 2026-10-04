using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Xunit;
using BloodDonationMangementSystem.Repositories;
using BloodDonationManagementSystem.Models;

namespace Tests
{
    public class IntegrationTest
    {
        private readonly IConfiguration _configuration;

        public IntegrationTest()
        {
            // Build configuration so DonorRepository can access appsettings or memory connection string
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "ConnectionStrings:DefaultConnection", "Server=(localdb)\\MSSQLLocalDB;Database=MediBlooddb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;" }
                })
                .Build();
        }

        [Fact]
        public void GetDonorById_ReturnsDonor_FromDatabase()
        {
            // Pass the configuration into the repository constructor
            var repo = new DonorRepository(_configuration);
            string donorId = "01595e00-6aee-4ce3-a7a2-02b99e284e6a";

            var donor = repo.GetDonorById(donorId);

            Assert.NotNull(donor);
            Assert.Equal(donorId, donor.Id);
        }

        [Fact]
        public void UpdateDonor_UpdatesData_InDatabase()
        {
            var repo = new DonorRepository(_configuration);
            string expectedName = "Tahir";
            string expectedBloodGroup = "B+";

            var donor = new User
            {
                Id = "01595e00-6aee-4ce3-a7a2-02b99e284e6a",
                FullName = expectedName,
                Age = 25,
                Contact = "03001234567",
                BloodGroup = expectedBloodGroup,
                CNIC = "12345-6789012-3",
                Availability = true
            };

            repo.UpdateDonor(donor);

            var updatedDonor = repo.GetDonorById(donor.Id);

            Assert.NotNull(updatedDonor);
            Assert.Equal(expectedName, updatedDonor.FullName); // Matched with "Tahir"
            Assert.Equal(expectedBloodGroup, updatedDonor.BloodGroup);
        }
    }
}