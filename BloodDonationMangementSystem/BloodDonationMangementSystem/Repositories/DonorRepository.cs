using BloodDonationManagementSystem.Interfaces;
using BloodDonationManagementSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BloodDonationMangementSystem.Repositories
{
    public class DonorRepository : IDonorRepository
    {
        private readonly string _constr;

        // Inject IConfiguration to pull connection string from appsettings.json
        public DonorRepository(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        // Get donor by id
        public User GetDonorById(string id)
        {
            using (var con = new SqlConnection(_constr))
            {
                string query = "SELECT * FROM AspNetUsers WHERE Id = @Id";
                return con.QuerySingleOrDefault<User>(query, new { Id = id });
            }
        }

        // Update donor info
        public void UpdateDonor(User user)
        {
            using (var con = new SqlConnection(_constr))
            {
                string query = @"UPDATE AspNetUsers 
                                 SET FullName = @FullName,
                                     Age = @Age,
                                     Contact = @Contact,
                                     BloodGroup = @BloodGroup,
                                     CNIC = @CNIC,
                                     Availability = @Availability
                                 WHERE Id = @Id";

                con.Execute(query, user);
            }
        }

        // Insert new donation request
        public void AddDonationRequest(DonationRequest dr, string id)
        {
            // Bind both DonorId and UserId to the logged-in user's ID
            dr.DonorId = id;
            dr.UserId = id;

            // Fallback for RequestDate
            if (dr.RequestDate == default)
            {
                dr.RequestDate = DateTime.Now;
            }

            using (var con = new SqlConnection(_constr))
            {
                // Include UserId in the column list and values clause
                string query = @"INSERT INTO DonationRequest (FullName, BloodGroup, DonorId, UserId, Reason, Status, RequestDate) 
                         VALUES (@FullName, @BloodGroup, @DonorId, @UserId, @Reason, @Status, @RequestDate)";

                int result = con.Execute(query, dr);
                Console.WriteLine("Number of rows inserted: " + result);
            }
        }

        // Get donation request by donor ID
        public DonationRequest GetDonationRequest(string id)
        {
            using (var con = new SqlConnection(_constr))
            {
                string query = "SELECT * FROM DonationRequest WHERE DonorId = @DonorId";
                return con.QuerySingleOrDefault<DonationRequest>(query, new { DonorId = id });
            }
        }
    }
}