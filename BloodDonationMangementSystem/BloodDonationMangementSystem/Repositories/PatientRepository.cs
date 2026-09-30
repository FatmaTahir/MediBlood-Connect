using BloodDonationManagementSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using BloodDonationManagementSystem.Interfaces;

namespace BloodDonationMangementSystem.Repositories
{
    public class PatientRepository:IPatientRepository
    {
        string constr = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=MediBlooddb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        
        //insert new patient
        public void AddPatient(Patient p)
        {
            SqlConnection con=new SqlConnection(constr);
            con.Open();
            string query = "Insert into Patients(FullName,Age) values(@name,@age)";
            int result=con.Execute(query,p);
            Console.WriteLine("Number of rows inserted: " + result);
            con.Close();
        }
        //new blood req
        
        public void AddBloodRequest(BloodRequest br)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Insert into BloodRequest(PatientName,PatientEmail,BloodGroup,UnitsRequired,Urgency,Reason,Status) values(@PatientName,@PatientEmail,@BloodGroup,@UnitsRequired,@Urgency,@Reason,@Status)";
            int result = con.Execute(query, br);
            Console.WriteLine("Number of rows inserted: " + result);
            con.Close();
        }
        //view blood req

        public List<BloodRequest> ViewBloodRequest(string email)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM BloodRequest WHERE PatientEmail = @Email";
                var req = con.Query<BloodRequest>(query, new { Email = email });
                return req.ToList();
            }
        }


        //matching donors
        
        public List<User> ViewMatchingDonors(string bloodGroup )
        {
            using (SqlConnection con = new SqlConnection(constr))
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
            AND (@BloodGroup = '' OR u.BloodGroup = @BloodGroup)
            AND (u.Availability = 1)
            ORDER BY u.FullName";

                return con.Query<User>(query, new
                {
                    BloodGroup = bloodGroup,
                    
                }).ToList();
            }
        }
        //get approved request
        public BloodRequest GetApprovedRequest(string email)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = @"SELECT TOP 1 *
                         FROM BloodRequest
                         WHERE PatientEmail = @Email
                         AND Status = 'Approved'";

                return con.QueryFirstOrDefault<BloodRequest>(query, new { Email = email });
            }
        }



    }
}
