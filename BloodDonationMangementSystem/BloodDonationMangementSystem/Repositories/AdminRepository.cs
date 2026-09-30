using Azure.Core;
using BloodDonationManagementSystem.Interfaces;
using BloodDonationManagementSystem.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;


namespace BloodDonationMangementSystem.Repositories
{
    public class AdminRepository :IAdminRepository
    {
        string constr = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=MediBlooddb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

        //view blood reqs
        public List<BloodRequest> ViewBloodRequests(string bloodGroup, string status)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = @"SELECT * FROM BloodRequest
                         WHERE (@BloodGroup IS NULL OR BloodGroup = @BloodGroup)
                         AND (@Status IS NULL OR Status = @Status)
                         ORDER BY BloodRequestId DESC";

                var result = con.Query<BloodRequest>(query, new
                {
                    BloodGroup = string.IsNullOrEmpty(bloodGroup) ? null : bloodGroup,
                    Status = string.IsNullOrEmpty(status) ? null : status
                });

                return result.ToList();
            }
        }
        public List<DonationRequest> ViewDonationRequests(string bloodGroup, string status)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = @"
            SELECT * FROM DonationRequest
            WHERE (@BloodGroup IS NULL OR BloodGroup = @BloodGroup)
            AND (@Status IS NULL OR Status = @Status)
            ORDER BY DonationId DESC";

                var result = con.Query<DonationRequest>(query, new
                {
                    BloodGroup = string.IsNullOrEmpty(bloodGroup) ? null : bloodGroup,
                    Status = string.IsNullOrEmpty(status) ? null : status
                });

                return result.ToList();
            }
        }

        public List<User> ViewDonors(string bloodGroup = "", string availability = "")
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = @"
            SELECT 
                u.Id,
                u.FullName,
                u.Age,
                u.BloodGroup,
                u.Contact,
                u.Availability
            FROM AspNetUsers u
            INNER JOIN AspNetUserRoles ur ON u.Id = ur.UserId
            INNER JOIN AspNetRoles r ON ur.RoleId = r.Id
            WHERE r.Name = 'Donor'
            AND (@BloodGroup = '' OR u.BloodGroup = @BloodGroup)
            AND (
                @Availability = '' OR
                (@Availability = 'yes' AND u.Availability = 1) OR
                (@Availability = 'no' AND u.Availability = 0)
            )
            ORDER BY u.FullName";

                return con.Query<User>(query, new
                {
                    BloodGroup = bloodGroup,
                    Availability = availability
                }).ToList();
            }
        }

        // donors count
        public int GetTotalDonationRequests()
        {
            using var con = new SqlConnection(constr);
            return con.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM DonationRequest"
            );
        }
        // blood req count
        public int GetTotalBloodRequests()
        {
            using var con = new SqlConnection(constr);
            return con.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM BloodRequest"
            );
        }
        public void UpdateDonationRequestStatus(int requestId)
        {
       
            using (var con = new SqlConnection(constr))
            {
                con.Open();
                var cmd = new SqlCommand("UPDATE DonationRequest SET Status='Approved' WHERE DonationId=@Id", con);
                cmd.Parameters.AddWithValue("@Id", requestId);
                cmd.ExecuteNonQuery();
            }
        }

        public string GetDonorIdByRequest(int requestId)
        {
            using (var con = new SqlConnection(constr))
            {
                con.Open();
                var cmd = new SqlCommand(
                    "SELECT DonorId FROM DonationRequest WHERE DonationId=@Id", con);
                cmd.Parameters.AddWithValue("@Id", requestId);

                var result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : null;
            }
        }
        public void ApproveBloodRequest(int requestId)
        {
            using var con = new SqlConnection(constr);
            con.Execute(
                "UPDATE BloodRequest SET Status = 'Approved' WHERE BloodRequestId = @Id",
                new { Id = requestId }
            );
        }

        public void RejectBloodRequest(int requestId)
        {
            using var con = new SqlConnection(constr);
            con.Execute(
                "UPDATE BloodRequest SET Status = 'Rejected' WHERE BloodRequestId = @Id",
                new { Id = requestId }
            );
        }

        public string GetPatientIdByBloodRequest(int requestId)
        {
            using var con = new SqlConnection(constr);

            return con.QueryFirstOrDefault<string>(
                @"SELECT U.Id FROM BloodRequest BR JOIN AspNetUsers U ON BR.PatientEmail = U.Email WHERE BR.BloodRequestId = @Id", new { Id = requestId }
            );

        }
        public User GetDonorById(string id)
        {
            using (var con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM AspNetUsers WHERE Id = @Id";
                return con.QuerySingleOrDefault<User>(query, new { Id = id });
            }
        }
        public DonationRequest GetDonationById(int requestid)
        {
            using(var con = new SqlConnection(constr))
            {
                string query = "Select * From DonationRequest where DonationId=@id";
                return con.QuerySingleOrDefault<DonationRequest>(query, new { id = requestid });
            }
        }
        public void ApproveDonation(int requestId)
        {
            using var con = new SqlConnection(constr);
            con.Execute(
                "UPDATE DonationRequest SET Status = 'Approved' WHERE DonationId = @Id",
                new { Id = requestId }
            );
        }

        public void RejectDonation(int requestId)
        {
            using var con = new SqlConnection(constr);
            con.Execute(
                "UPDATE DonationRequest SET Status = 'Rejected' WHERE DonationId = @Id",
                new { Id = requestId }
            );
        }




    }
}
