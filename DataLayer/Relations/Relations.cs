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
        public static DataRow GetByID(int relationID)
        {
            // Define the connection string
            string connectionString = Helpers.GetDefaultConnectionString();

            // Define the SQL query
            string query = @"SELECT * FROM [Relations] WHERE RelationID = @RelationID;";

            // Create a DataTable to hold the results
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the RelationID parameter to the query
                        command.Parameters.AddWithValue("@RelationID", relationID);

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
                // Log the error (optional)
                Console.WriteLine($"Error: {ex.Message}");

                // Optionally, rethrow the exception or handle it as needed
                throw new Exception("Failed to retrieve relation by ID.", ex);
            }

            // Return the DataTable
            return dataTable.Rows[0];
        }

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

        public static bool Update(RelationItemModels relation)
        {
            // Define the connection string
            string connectionString = Helpers.GetDefaultConnectionString();

            // Define the SQL query
            string query = @"
                UPDATE [Relations]
                SET 
                    [SourceTableName] = @SourceTable,
                    [SourceColumnName] = @SourceColumn,
                    [DestinationTableName] = @DestinationTable,
                    [DestinationColumnName] = @DestinationColumn,
                    [EventID] = @EventID
                WHERE 
                    [RelationID] = @RelationID;";

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
                        command.Parameters.AddWithValue("@RelationID", relation.ID);

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

        public static bool Delete(int relationID)
        {
            // Define the connection string
            string connectionString = Helpers.GetDefaultConnectionString();

            // Define the SQL query
            string query = "DELETE FROM [Relations] WHERE RelationID = @RelationID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the RelationID parameter to the query
                        command.Parameters.AddWithValue("@RelationID", relationID);

                        // Execute the query and get the number of rows affected
                        int rowsAffected = command.ExecuteNonQuery();

                        // Return true if at least one row was deleted
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

        public static DataTable Search(string search)
        {
            // Define the connection string
            string connectionString = Helpers.GetDefaultConnectionString();

            // Create a DataTable to hold the results
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Build the SQL query dynamically based on the provided criteria
                    string query = "  SELECT * FROM [Relations] WHERE SourceTableName LIKE @Search or DestinationTableName LIKE @Search or SourceColumnName like @Search or DestinationColumnName like @Search";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@Search", $"%{search}%");


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

        public static bool ValidateMappings(List<RelationItemModels> mappings, List<ColumnInfo> destinationColumns)
        {
            foreach (var column in destinationColumns)
            {
                if (!column.IsNullable)
                {
                    // Check if the non-nullable column is mapped
                    bool isMapped = mappings.Any(m => m.DestinationColumnName.Equals(column.ColumnName, StringComparison.OrdinalIgnoreCase));

                    if (!isMapped)
                    {
                        Console.WriteLine($"Error: Non-nullable column '{column.ColumnName}' is not mapped.");
                        return false;
                    }
                }
            }

            return true;
        }

        public static List<ColumnInfo> GetColumnInfo(string connectionString, string tableName)
        {
            var columns = new List<ColumnInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                                    SELECT 
                                        COLUMN_NAME, 
                                        IS_NULLABLE, 
                                        DATA_TYPE 
                                    FROM 
                                        INFORMATION_SCHEMA.COLUMNS 
                                    WHERE 
                                        TABLE_NAME = @TableName;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TableName", tableName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                columns.Add(new ColumnInfo
                                {
                                    ColumnName = reader["COLUMN_NAME"].ToString(),
                                    IsNullable = reader["IS_NULLABLE"].ToString() == "YES",
                                    DataType = reader["DATA_TYPE"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving column info for table {tableName}: {ex.Message}");
                return columns;
            }

            return columns;
        }


        public static async Task TransferDataAsync(IProgress<int> progress , List<RelationItemModels> mappList , string oldDbConnectionString , string newDbConnectionString)
        {
            // Retrieve mappings from the [Relations] table
           // List<RelationItemModels> mappings = GetRelationsByEventID(eventID);

            // Define connection strings for the old and new databases
            //string oldDbConnectionString = Helpers.CreateConnectionString();
            //string newDbConnectionString = "YourNewDbConnectionString";

            int totalMappings = mappList.Count;
            int completedMappings = 0;

            foreach (var mapping in mappList)
            {
                try
                {
                    // Step 1: Retrieve column information for the destination table
                    List<ColumnInfo> destinationColumns = GetColumnInfo(newDbConnectionString, mapping.DestinationTableName);

                    // Step 2: Validate the mappings
                    if (!ValidateMappings(mappList, destinationColumns))
                    {
                        throw new Exception($"Validation failed for table '{mapping.DestinationTableName}'.");
                    }

                    // Step 3: Read data from the source table in the old database
                    using (SqlConnection oldDbConnection = new SqlConnection(oldDbConnectionString))
                    {
                        await oldDbConnection.OpenAsync();

                        string selectQuery = $"SELECT [{mapping.SourceColumnName}] FROM [{mapping.SourceTableName}];";
                        using (SqlCommand selectCommand = new SqlCommand(selectQuery, oldDbConnection))
                        {
                            using (SqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                            {
                                // Step 4: Use SqlBulkCopy to insert data into the destination table
                                using (SqlConnection newDbConnection = new SqlConnection(newDbConnectionString))
                                {
                                    await newDbConnection.OpenAsync();

                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(newDbConnection))
                                    {
                                        bulkCopy.DestinationTableName = $"[{mapping.DestinationTableName}]";

                                        // Map the source column to the destination column
                                        bulkCopy.ColumnMappings.Add(mapping.SourceColumnName, mapping.DestinationColumnName);

                                        // Write the data to the destination table
                                        await bulkCopy.WriteToServerAsync(reader);
                                    }
                                }
                            }
                        }
                    }

                    // Update progress
                    completedMappings++;
                    int progressPercentage = (int)((double)completedMappings / totalMappings * 100);
                    progress.Report(progressPercentage);
                }
                catch (Exception ex)
                {
                    // Log the error (optional)
                    Console.WriteLine($"Error transferring data for mapping {mapping.SourceTableName} -> {mapping.DestinationTableName}: {ex.Message}");
                }
            }
        }
    }
}
