using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataLayer
{
    public static class AuthenticationType
    {
        public static DataRow GetByID(int id)
        {
            // Define the connection string (use your actual connection string)
            string connectionString = Helpers.GetDefaultConnectionString(); // Assuming you have a Helper class to get the connection string

            // Define the SQL query
            string query = @"SELECT * FROM [AuthTypes] where AuthTypeID = @ID";

            // Create a DataTable to hold the results
            DataTable dataTable = new DataTable();

            try
            {
                // Create and open a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Create a SqlCommand to execute the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        // Create a SqlDataAdapter to fill the DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Fill the DataTable with the query results
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error or throw it)
                throw new Exception("Failed to retrieve data from the database.", ex);
            }

            // Return the DataTable
            return dataTable.Rows[0];
        }
    }
}
