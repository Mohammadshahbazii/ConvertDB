using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public enum EventType
    {
        Deactive,
        Processing,
        Done
    }

    public static class Helpers
    {

        public static string GetEventType(int typeID)
        {
            switch (typeID)
            {
                case 0:
                    return "انجام نشده";
                case 1:
                    return "در حال انجام";
                case 2:
                    return "اتمام عملیات";
                default:
                    return "نا معلوم";
            }
        }

        public static bool CanProcess(string type)
        {
            switch (type)
            {
                case "انجام نشده":
                    return true;
                case "در حال انجام":
                    return false;
                case "اتمام عملیات":
                    return false;
                default:
                    return false;
            }
        }

        #region AuthType
        public static List<DataBaseAuthenticationType> GetAuthTypes()
        {
            var authenticationTypeList = new List<DataBaseAuthenticationType>()
            {
                new DataBaseAuthenticationType(){ TypeID = 1 , Type = "Windows Authentication" },
                new DataBaseAuthenticationType(){TypeID = 2 , Type ="SQL Server Authentication"}
            };
            return authenticationTypeList;
        }
        #endregion AuthType

        #region ConnectionString
        public static string GetServerConnectionString(string serverAddress)
        {
            return $"Server={serverAddress};MultipleActiveResultSets=True;Trusted_Connection=True;";
        }

        public static string CreateConnectionString(DataBaseInfo dataBaseInfo)
        {
            string connection;
            if (dataBaseInfo.AuthenticationType.Type == "SQL Server Authentication")
            {
                connection = $"data source={dataBaseInfo.ServerAddress.Replace("\\\\", "\\")};User ID ={dataBaseInfo.Username};Password ={dataBaseInfo.Password};initial catalog={dataBaseInfo.DataBaseName};Trusted_Connection=False;integrated security=False;MultipleActiveResultSets=true;TrustServerCertificate=True;";
            }
            else
            {
                connection = $"data source={dataBaseInfo.ServerAddress.Replace("\\\\", "\\")};initial catalog={dataBaseInfo.DataBaseName};integrated security=True;MultipleActiveResultSets=True;Trusted_Connection=True;TrustServerCertificate=True";
            }
            return connection;
        }

        public static string GetDefaultConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultDatabase"].ConnectionString;
        }


        public static DataBaseInfo ParseConnectionString(string connectionString)
        {
            DataBaseInfo dataBaseInfo = new DataBaseInfo();
            try
            {
                // Use SqlConnectionStringBuilder to parse the connection string
                var builder = new SqlConnectionStringBuilder(connectionString);

                // Access individual components
                dataBaseInfo.ServerAddress = builder.DataSource;
                dataBaseInfo.DataBaseName = builder.InitialCatalog;
                dataBaseInfo.Username = builder.UserID;
                dataBaseInfo.Password = builder.Password;
                return dataBaseInfo;

            }
            catch (Exception ex)
            {
                return dataBaseInfo = new DataBaseInfo();
            }
        }

        public static void SaveConnectionStringToConfig(string name, string connectionString)
        {
            // Open the app.config file
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Add or update the connection string
            if (config.ConnectionStrings.ConnectionStrings[name] != null)
            {
                config.ConnectionStrings.ConnectionStrings[name].ConnectionString = connectionString;
            }
            else
            {
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(name, connectionString));
            }

            // Save the changes
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        public static bool TestConnectionString(string connectionString)
        {
            try
            {
                // Test the connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion ConnectionString

        #region CopyDB

        public static void CreateDatabaseIfNotExists(string targetServerConnectionString, string targetDatabaseName)
        {
            using (SqlConnection conn = new SqlConnection(targetServerConnectionString))
            {
                conn.Open();

                // Check if the database exists
                string checkDatabaseQuery = $"SELECT COUNT(*) FROM sys.databases WHERE name = '{targetDatabaseName}'";
                using (SqlCommand cmd = new SqlCommand(checkDatabaseQuery, conn))
                {
                    int databaseExists = (int)cmd.ExecuteScalar();

                    if (databaseExists == 0)
                    {
                        // Create the database if it doesn't exist
                        string createDatabaseQuery = $"CREATE DATABASE {targetDatabaseName}";
                        using (SqlCommand createCmd = new SqlCommand(createDatabaseQuery, conn))
                        {
                            createCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public static void CopySchema(string sourceConnectionString, string targetConnectionString)
        {
            using (SqlConnection sourceConn = new SqlConnection(sourceConnectionString))
            using (SqlConnection targetConn = new SqlConnection(targetConnectionString))
            {
                sourceConn.Open();
                targetConn.Open();

                // Get all tables from the source database
                DataTable tables = sourceConn.GetSchema("Tables");

                foreach (DataRow table in tables.Rows)
                {
                    string tableName = table["TABLE_NAME"].ToString();

                    // Get the schema for the current table
                    string createTableQuery = GetCreateTableQuery(sourceConn, tableName);

                    // Execute the create table query in the target database
                    using (SqlCommand cmd = new SqlCommand(createTableQuery, targetConn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static string GetCreateTableQuery(SqlConnection connection, string tableName)
        {
            string query = @"
                            SELECT 
                                COLUMN_NAME, 
                                DATA_TYPE, 
                                CHARACTER_MAXIMUM_LENGTH, 
                                IS_NULLABLE
                            FROM INFORMATION_SCHEMA.COLUMNS
                            WHERE TABLE_NAME = @TableName";
            DataTable columns = new DataTable();

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(columns);
                }
            }

            string createTableQuery = $"CREATE TABLE {tableName} (";
            foreach (DataRow column in columns.Rows)
            {
                string columnName = column["COLUMN_NAME"].ToString();
                string dataType = column["DATA_TYPE"].ToString();
                string isNullable = column["IS_NULLABLE"].ToString();
                int characterMaxLength = column["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value
                    ? Convert.ToInt32(column["CHARACTER_MAXIMUM_LENGTH"])
                    : -1;

                // Append the column definition to the CREATE TABLE query
                createTableQuery += $"{columnName} {dataType}";

                // Add size for string types (e.g., NVARCHAR(150))
                if (characterMaxLength > 0 && (dataType.ToUpper() == "NVARCHAR" || dataType.ToUpper() == "VARCHAR"))
                {
                    createTableQuery += $"({characterMaxLength})";
                }
                else if (characterMaxLength == -1 && (dataType.ToUpper() == "NVARCHAR" || dataType.ToUpper() == "VARCHAR"))
                {
                    createTableQuery += "(MAX)"; // Handle MAX sizes
                }

                // Add NOT NULL if applicable
                createTableQuery += $" {(isNullable == "NO" ? "NOT NULL" : "")}, ";
            }
            createTableQuery = createTableQuery.TrimEnd(',', ' ') + ")";

            return createTableQuery;
        }
        public static void CopyData(string sourceConnectionString, string targetConnectionString)
        {
            using (SqlConnection sourceConn = new SqlConnection(sourceConnectionString))
            using (SqlConnection targetConn = new SqlConnection(targetConnectionString))
            {
                sourceConn.Open();
                targetConn.Open();

                // Get all tables from the source database
                DataTable tables = sourceConn.GetSchema("Tables");

                foreach (DataRow table in tables.Rows)
                {
                    string tableName = table["TABLE_NAME"].ToString();

                    // Read data from the source table
                    string selectQuery = $"SELECT * FROM {tableName}";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, sourceConn))
                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        // Get the column sizes for the target table
                        DataTable columnSizes = GetColumnSizes(targetConn, tableName);

                        // Insert data into the target table
                        while (reader.Read())
                        {
                            string insertQuery = $"INSERT INTO {tableName} VALUES (";
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                string columnValue = reader[i].ToString();

                                // Truncate the value if it exceeds the column size
                                int columnSize = GetColumnSize(columnSizes, columnName);
                                if (columnSize > 0 && columnValue.Length > columnSize)
                                {
                                    columnValue = columnValue.Substring(0, columnSize);
                                }

                                // Check if the column is Unicode (NVARCHAR/NCHAR/NTEXT)
                                bool isUnicode = IsUnicodeColumn(columnSizes, columnName);

                                // Prefix Unicode strings with N''
                                if (isUnicode)
                                {
                                    columnValue = $"N'{columnValue.Replace("'", "''")}'";
                                }
                                else
                                {
                                    columnValue = $"'{columnValue.Replace("'", "''")}'";
                                }

                                insertQuery += $"{columnValue}, ";
                            }
                            insertQuery = insertQuery.TrimEnd(',', ' ') + ")";

                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, targetConn))
                            {
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        public static bool IsUnicodeColumn(DataTable columnSizes, string columnName)
        {
            foreach (DataRow row in columnSizes.Rows)
            {
                if (row["COLUMN_NAME"].ToString() == columnName)
                {
                    string dataType = row["DATA_TYPE"].ToString().ToUpper();
                    return dataType.StartsWith("N"); // NVARCHAR, NCHAR, NTEXT, etc.
                }
            }
            return false;
        }

        public static DataTable GetColumnSizes(SqlConnection connection, string tableName)
        {
            string query = @"
        SELECT COLUMN_NAME, CHARACTER_MAXIMUM_LENGTH, DATA_TYPE
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = @TableName";
            DataTable columnSizes = new DataTable();

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(columnSizes);
                }
            }

            return columnSizes;
        }
        public static int GetColumnSize(DataTable columnSizes, string columnName)
        {
            foreach (DataRow row in columnSizes.Rows)
            {
                if (row["COLUMN_NAME"].ToString() == columnName)
                {
                    // Handle NVARCHAR(MAX) or VARCHAR(MAX)
                    if (row["CHARACTER_MAXIMUM_LENGTH"] == DBNull.Value || Convert.ToInt64(row["CHARACTER_MAXIMUM_LENGTH"]) == -1)
                    {
                        return int.MaxValue; // Treat as unlimited size
                    }

                    // Return the actual size for NVARCHAR or VARCHAR
                    return Convert.ToInt32(row["CHARACTER_MAXIMUM_LENGTH"]);
                }
            }
            return -1; // Column not found
        }

        #endregion CopyDB

        #region BackupDB
        public static void BackupDatabase(string filePath, DataBaseInfo dataBaseInfo)
        {
            var connectionString = Helpers.CreateConnectionString(dataBaseInfo);
            string backupCommand = $@"BACKUP DATABASE [{dataBaseInfo.DataBaseName}] TO DISK = '{filePath}' WITH FORMAT, MEDIANAME = 'SQLServerBackups', NAME = 'Full Backup';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(backupCommand, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        #endregion BackupDB

    }
}
