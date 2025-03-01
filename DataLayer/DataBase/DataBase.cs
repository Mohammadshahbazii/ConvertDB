using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataLayer
{
    public static class DataBase
    {
        public static List<string> GetTableNames(string connectionString)
        {
            var tableNames = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tableNames.Add(reader["TABLE_NAME"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return tableNames;
            }

            return tableNames;
        }

        public static List<string> GetColumnNames(string connectionString, string tableName)
        {
            var columnNames = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TableName", tableName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                columnNames.Add(reader["COLUMN_NAME"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return columnNames;
            }

            return columnNames;
        }

        public static bool Update(DataBaseInfo dataBaseInfo)
        {
            // Define the connection string
            string connectionString = Helpers.GetDefaultConnectionString();

            // Define the SQL query
            string query = @"
                UPDATE [DataBases]
                SET 
                    [ServerName] = @ServerName,
                    [DBName] = @DBName,
                    [AuthTypeID] = @AuthTypeID,
                    [Username] = @Username,
                    [Password] = @Password
                WHERE 
                    [DBID] = @DBID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@ServerName", dataBaseInfo.ServerAddress);
                        command.Parameters.AddWithValue("@DBName", dataBaseInfo.DataBaseName);
                        command.Parameters.AddWithValue("@AuthTypeID", dataBaseInfo.AuthenticationType.TypeID);
                        if(dataBaseInfo.AuthenticationType.TypeID == 1)
                        {
                            command.Parameters.AddWithValue("@Username", DBNull.Value);
                            command.Parameters.AddWithValue("@Password", DBNull.Value);
                        } 
                        else 
                        {
                            command.Parameters.AddWithValue("@Username", dataBaseInfo.Username);
                            command.Parameters.AddWithValue("@Password", dataBaseInfo.Password);
                        }
                        
                        command.Parameters.AddWithValue("@DBID", dataBaseInfo.ID);

                        // Execute the query and get the number of rows affected
                        int rowsAffected = command.ExecuteNonQuery();

                        // Return true if at least one row was updated
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

        public static bool Delete(int id)
        {
            // Define the connection string
            string connectionString = Helpers.GetDefaultConnectionString();

            // Define the SQL query
            string query = "DELETE FROM [DataBases] WHERE DBID = @ID;";

            try
            {
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
            catch (Exception ex)
            {

                // Return false if an exception occurs
                return false;
            }
        }

        public static int Create(DataBaseInfo dataBaseInfo)
        {
            // Define the connection string
            string connectionString = Helpers.GetDefaultConnectionString();

            // Define the SQL query
            string query = @"
                INSERT INTO [DataBases] (
                    [ServerName],
                    [DBName],
                    [AuthTypeID],
                    [Username],
                    [Password]
                )
                VALUES (
                    @ServerName,
                    @DBName,
                    @AuthTypeID,
                    @Username,
                    @Password
                );
                SELECT SCOPE_IDENTITY();"; // Retrieve the DBID of the newly inserted row

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@ServerName", dataBaseInfo.ServerAddress);
                        command.Parameters.AddWithValue("@DBName", dataBaseInfo.DataBaseName);
                        command.Parameters.AddWithValue("@AuthTypeID", dataBaseInfo.AuthenticationType.TypeID);

                        if (dataBaseInfo.AuthenticationType.TypeID == 1)
                        {
                            command.Parameters.AddWithValue("@Username", DBNull.Value);
                            command.Parameters.AddWithValue("@Password", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Username", dataBaseInfo.Username);
                            command.Parameters.AddWithValue("@Password", dataBaseInfo.Password);
                        }

                        // Execute the query and retrieve the DBID
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int dbID))
                        {
                            return dbID; // Return the DBID of the newly inserted row
                        }
                        else
                        {
                            throw new Exception("Failed to retrieve the DBID of the newly inserted row.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert data into the [DataBases] table.", ex);
            }
        }

        public static DataRow GetByID(int id)
        {
            // Define the connection string (use your actual connection string)
            string connectionString = Helpers.GetDefaultConnectionString(); // Assuming you have a Helper class to get the connection string

            // Define the SQL query
            string query = @"SELECT * FROM [DataBases] where DBID = @ID";

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


        public static DataBaseInfo GetDefaultDatabaseInfo()
        {
            // Define the connection string
            string connectionString = "";

            // Define the SQL query
            string query = @"
                 SELECT TOP (1) 
                    DB.ServerName AS ServerAddress,
                    DB.DBName AS DataBaseName,
                    DB.Username,
                    DB.Password,
                    AT.AuthTypeID AS AuthTypeID,
                    AT.Title AS AuthType,
                    (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE') AS TableCount
                FROM [DataBases] DB
                JOIN AuthTypes AT ON DB.AuthTypeID = AT.AuthTypeID
                WHERE DB.IsDefault = 'true'";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Map the query results to the DataBaseInfo object
                                return new DataBaseInfo
                                {
                                    ServerAddress = reader["ServerAddress"].ToString(),
                                    DataBaseName = reader["DataBaseName"].ToString(),
                                    Username = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    AuthenticationType = new DataBaseAuthenticationType
                                    {
                                        TypeID = Convert.ToInt32(reader["AuthTypeID"]),
                                        Type = reader["AuthType"].ToString()
                                    },
                                    TableCount = Convert.ToInt32(reader["TableCount"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve default database information.", ex);
            }

            return null; // No default database configuration found
        }
    }
}
