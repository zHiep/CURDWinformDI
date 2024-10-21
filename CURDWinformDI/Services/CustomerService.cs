using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CURDWinformDI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly string _connectionString;

        public CustomerService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable GetAllCustomers()
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                cnn.Open();
                string query = "SELECT * FROM dbo.Customer";
                SqlDataAdapter da = new SqlDataAdapter(query, cnn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void AddOrUpdateCustomer(string id, string name, string email, string phoneNumber)
        {
            string procedureName = string.IsNullOrEmpty(id) ? "dbo.spKH_insert" : "dbo.spKH_update";

            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (!string.IsNullOrEmpty(id))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                    }
                    else
                    {
                        var newId = SnowflakeIdGenerator.GenerateId();
                        cmd.Parameters.AddWithValue("@Id", newId);
                    }

                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCustomer(string id)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("spKH_delete", cnn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
