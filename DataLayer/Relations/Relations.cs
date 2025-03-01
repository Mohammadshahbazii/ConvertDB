using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Domain;
using System.Runtime.Remoting.Messaging;

namespace DataLayer
{
    public static class Relations
    {
        public static DataTable GetRelationsByEventID(int eventID)
        {
            // Define the connection string
            string connectionString = Helpers.GetDefaultConnectionString();

            // Define the SQL query
            string query = @"SELECT * FROM [Relations] WHERE EventID = @EventID;";

            // Create a DataTable to hold the results
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the EventID parameter to the query
                        command.Parameters.AddWithValue("@EventID", eventID);

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
                return dataTable;
            }

            // Return the DataTable
            return dataTable;
        }

        public static bool Create(RelationItemModels relation)
        {
            // Define the connection string
            string connectionString = Helpers.GetDefaultConnectionString();

            // Define the SQL query
            string query = @"
                INSERT INTO [Relations] (
                    [SourceTableName]
                   ,[SourceColumnName]
                   ,[DestinationTableName]
                   ,[DestinationColumnName]
                   ,[EventID]
                )
                VALUES (
                    @SourceTable,
                    @SourceColumn,
                    @DestinationTable,
                    @DestinationColumn,
                    @EventID
                );";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@SourceTable", relation.SourceTableName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SourceColumn", relation.SourceColumnName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DestinationTable", relation.DestinationTableName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DestinationColumn", relation.DestinationColumnName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@EventID", relation.EventID);

                        // Execute the query and get the number of rows affected
                        int rowsAffected = command.ExecuteNonQuery();

                        // Return true if at least one row was inserted
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
    }
}
