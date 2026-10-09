using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BloodDonationManagementSystem.Interfaces;
using BloodDonationManagementSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BloodDonationMangementSystem.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IConfiguration _configuration;

        // Inject IConfiguration to dynamically read connection strings from appsettings.json
        public PatientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("BloodDonationManagementSystemContextConnection"));
        }

        // Insert new patient
        public void AddPatient(Patient p)
        {
            using (IDbConnection con = CreateConnection())
            {
                // Fixed SQL parameter names to match Patient class properties (@FullName, @Age)
                string query = "INSERT INTO Patients (FullName, Age) VALUES (@FullName, @Age)";
                int result = con.Execute(query, p);
                Console.WriteLine("Number of rows inserted: " + result);
            }
        }

        // New blood request
        // New blood request
        public void AddBloodRequest(BloodRequest br)
        {
            using (IDbConnection con = CreateConnection())
            {
                string query = @"INSERT INTO BloodRequest 
                        (UserId, PatientName, PatientEmail, BloodGroup, UnitsRequired, Urgency, Reason, Status, RequestDate) 
                        VALUES 
                        (@UserId, @PatientName, @PatientEmail, @BloodGroup, @UnitsRequired, @Urgency, @Reason, @Status, @RequestDate)";

                int result = con.Execute(query, br);
                Console.WriteLine("Number of rows inserted: " + result);
            }
        }

        // View blood requests for patient
        public List<BloodRequest> ViewBloodRequest(string email)
        {
            using (IDbConnection con = CreateConnection())
            {
                string query = "SELECT * FROM BloodRequest WHERE PatientEmail = @Email";

                // Returns empty list [] if user hasn't submitted any blood request
                var req = con.Query<BloodRequest>(query, new { Email = email });
                return req?.ToList() ?? new List<BloodRequest>();
            }
        }

        // View matching donors
        public List<User> ViewMatchingDonors(string bloodGroup)
        {
            using (IDbConnection con = CreateConnection())
            {
                string query = @"
                    SELECT 
                        u.FullName,
                        u.BloodGroup,
                        u.Contact,
                        u.Availability
                    FROM AspNetUsers u
                    INNER JOIN AspNetUserRoles ur ON u.Id = ur.UserId
                    INNER JOIN AspNetRoles r ON ur.RoleId = r.Id
                    WHERE r.Name = 'Donor'
                    AND (@BloodGroup IS NULL OR @BloodGroup = '' OR u.BloodGroup = @BloodGroup)
                    AND (u.Availability = 1)
                    ORDER BY u.FullName";

                var donors = con.Query<User>(query, new { BloodGroup = bloodGroup ?? string.Empty });
                return donors?.ToList() ?? new List<User>();
            }
        }

        // Get approved request
        public BloodRequest GetApprovedRequest(string email)
        {
            using (IDbConnection con = CreateConnection())
            {
                string query = @"SELECT TOP 1 *
                                 FROM BloodRequest
                                 WHERE PatientEmail = @Email
                                 AND Status = 'Approved'";

                // Returns null if no approved request exists yet
                return con.QueryFirstOrDefault<BloodRequest>(query, new { Email = email });
            }
        }
    }
}