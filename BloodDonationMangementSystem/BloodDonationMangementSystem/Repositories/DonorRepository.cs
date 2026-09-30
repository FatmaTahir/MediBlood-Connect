using BloodDonationManagementSystem.Interfaces;
using BloodDonationManagementSystem.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Drawing;
namespace BloodDonationMangementSystem.Repositories
{
    public class DonorRepository:IDonorRepository
    {
        string constr = "Data Source=FATIMA\\SQLEXPRESS;Initial Catalog=MediBlooddb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        //donor by id
        public User GetDonorById(string id)
        {
            using (var con = new SqlConnection(constr))
            {
                string query = "SELECT * FROM AspNetUsers WHERE Id = @Id";
                return con.QuerySingleOrDefault<User>(query, new { Id = id });
            }
        }

        //update donor info
        public void UpdateDonor(User user)
        {
            using (var con = new SqlConnection(constr))
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
        //insert new donation req
        public void AddDonationRequest(DonationRequest dr,string id)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string query = "Insert into DonationRequest(FullName,BloodGroup,DonorId,Reason,Status) values(@FullName,@BloodGroup,@DonorId,@Reason,@Status)";
            dr.DonorId = id;
            int result = con.Execute(query, dr);
            Console.WriteLine("Number of rows inserted: " + result);
            con.Close();
        }

        
        public DonationRequest GetDonationRequest(string id) {
            using (var con = new SqlConnection(constr))
            {
                string query = "SELECT* FROM DonationRequest WHERE DonorId = @DonorId";
                return con.QuerySingleOrDefault<DonationRequest>(query, new { DonorId = id });
            }
        }

        }


    }

