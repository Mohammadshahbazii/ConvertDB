using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Domain;

namespace DataLayer
{
    public static class Events
    {

        public static DataTable GetEventsData()
        {
            // Define the connection string (use your actual connection string)
            string connectionString = Helpers.GetDefaultConnectionString(); // Assuming you have a Helper class to get the connection string

            // Define the SQL query
            string query = @" SELECT * FROM [Events]";

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
            return dataTable;
        }

        public static DataRow GetByID(int id)
        {
            // Define the connection string (use your actual connection string)
            string connectionString = Helpers.GetDefaultConnectionString(); // Assuming you have a Helper class to get the connection string

            // Define the SQL query
            string query = @"SELECT * FROM [Events] where EventID = @ID";

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

        public static bool Create(EventsItemModels eventItem)
        {
            // Define the connection string
            string connectionString = Helpers.GetDefaultConnectionString();

            // Define the SQL query
            string query = @"
                INSERT INTO [Events] (
                    [Title],
                    [CreateDate],
                    [Status],
                    [DBSourceID],
                    [DBDestinationID]
                )
                VALUES (
                    @Title,
                    @CreateDate,
                    @Status,
                    @DBSourceID,
                    @DBDestinationID
                );";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@Title", eventItem.Name);
                        command.Parameters.AddWithValue("@CreateDate", eventItem.CreateDate);
                        command.Parameters.AddWithValue("@Status", eventItem.Status);
                        command.Parameters.AddWithValue("@DBSourceID", eventItem.DBSourceID);
                        command.Parameters.AddWithValue("@DBDestinationID", eventItem.DBDestinationID);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Return true if at least one row was inserted
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Return false if an exception occurs
                return false;
            }
        }

        public static bool Update(EventsItemModels eventItem)
        {
            // Define the connection string
            string connectionString = Helpers.GetDefaultConnectionString();

            // Define the SQL query
            string query = @"
                UPDATE [Events]
                SET 
                    [Title] = @Title,
                    [Status] = @Status
                WHERE 
                    [EventID] = @EventID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@Title", eventItem.Name);
                        command.Parameters.AddWithValue("@Status", eventItem.Status);
                        command.Parameters.AddWithValue("@EventID", eventItem.ID);

                        // Execute the query and get the number of rows affected
                        int rowsAffected = command.ExecuteNonQuery();

                        // Return true if at least one row was updated
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine($"Error: {ex.Message}");

                // Return false if an exception occurs
                return false;
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                string connectionString = Helpers.GetDefaultConnectionString();

                // Define the SQL query
                string query = "delete Events where EventID = @ID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the ID parameter to the query
                        command.Parameters.AddWithValue("@ID", id);

                        // Execute the query and get the number of rows affected
                        int rowsAffected = command.ExecuteNonQuery();

                        // Return true if at least one row was deleted
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
