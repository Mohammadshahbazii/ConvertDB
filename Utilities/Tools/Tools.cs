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
                    if (!string.Equals(table["TABLE_TYPE"].ToString(), "BASE TABLE", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    string tableName = table["TABLE_NAME"].ToString();
                    string tableSchema = table["TABLE_SCHEMA"].ToString();

                    // Get the schema for the current table including keys
                    string createTableQuery = GetCreateTableQuery(sourceConn, tableSchema, tableName);

                    // Execute the create table query in the target database
                    using (SqlCommand cmd = new SqlCommand(createTableQuery, targetConn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

            }
        }

        public static string GetCreateTableQuery(SqlConnection connection, string tableSchema, string tableName)
        {
            string query = @"
                            SELECT
                                COLUMN_NAME,
                                DATA_TYPE,
                                CHARACTER_MAXIMUM_LENGTH,
                                NUMERIC_PRECISION,
                                NUMERIC_SCALE,
                                IS_NULLABLE
                            FROM INFORMATION_SCHEMA.COLUMNS
                            WHERE TABLE_NAME = @TableName AND TABLE_SCHEMA = @TableSchema";
            DataTable columns = new DataTable();

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@TableSchema", tableSchema);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(columns);
                }
            }

            string fullTableName = $"[{tableSchema}].[{tableName}]";
            string createTableQuery = $"CREATE TABLE {fullTableName} (";
            foreach (DataRow column in columns.Rows)
            {
                string columnName = column["COLUMN_NAME"].ToString();
                string dataType = column["DATA_TYPE"].ToString();
                string isNullable = column["IS_NULLABLE"].ToString();
                int characterMaxLength = column["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value
                    ? Convert.ToInt32(column["CHARACTER_MAXIMUM_LENGTH"])
                    : -1;
                int numericPrecision = column["NUMERIC_PRECISION"] != DBNull.Value
                    ? Convert.ToInt32(column["NUMERIC_PRECISION"])
                    : -1;
                int numericScale = column["NUMERIC_SCALE"] != DBNull.Value
                    ? Convert.ToInt32(column["NUMERIC_SCALE"])
                    : -1;

                // Append the column definition to the CREATE TABLE query
                createTableQuery += $"[{columnName}] {dataType}";

                // Add size for string types (e.g., NVARCHAR(150))
                if (characterMaxLength > 0 && (dataType.ToUpper() == "NVARCHAR" || dataType.ToUpper() == "VARCHAR"))
                {
                    createTableQuery += $"({characterMaxLength})";
                }
                else if (characterMaxLength == -1 && (dataType.ToUpper() == "NVARCHAR" || dataType.ToUpper() == "VARCHAR"))
                {
                    createTableQuery += "(MAX)"; // Handle MAX sizes
                }
                else if (numericPrecision > 0 && numericScale >= 0 &&
                         (dataType.ToUpper() == "DECIMAL" || dataType.ToUpper() == "NUMERIC"))
                {
                    createTableQuery += $"({numericPrecision}, {numericScale})";
                }

                // Add NOT NULL if applicable
                createTableQuery += $" {(isNullable == "NO" ? "NOT NULL" : "")}, ";
            }
            string primaryKeyDefinition = GetPrimaryKeyDefinition(connection, tableSchema, tableName);
            if (!string.IsNullOrEmpty(primaryKeyDefinition))
            {
                createTableQuery += primaryKeyDefinition + ", ";
            }
            createTableQuery = createTableQuery.TrimEnd(',', ' ') + ")";

            return createTableQuery;
        }
        private static string GetPrimaryKeyDefinition(SqlConnection connection, string tableSchema, string tableName)
        {
            string pkQuery = @"
                SELECT
                    KU.CONSTRAINT_NAME,
                    KU.COLUMN_NAME,
                    KU.ORDINAL_POSITION
                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU
                    ON TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME
                    AND TC.TABLE_NAME = KU.TABLE_NAME
                    AND TC.TABLE_SCHEMA = KU.TABLE_SCHEMA
                WHERE TC.TABLE_NAME = @TableName
                    AND TC.TABLE_SCHEMA = @TableSchema
                    AND TC.CONSTRAINT_TYPE = 'PRIMARY KEY'
                ORDER BY KU.ORDINAL_POSITION";

            DataTable primaryKeyInfo = new DataTable();
            using (SqlCommand cmd = new SqlCommand(pkQuery, connection))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@TableSchema", tableSchema);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(primaryKeyInfo);
                }
            }

            if (primaryKeyInfo.Rows.Count == 0)
            {
                return string.Empty;
            }

            string constraintName = primaryKeyInfo.Rows[0]["CONSTRAINT_NAME"].ToString();
            List<string> columns = new List<string>();
            foreach (DataRow row in primaryKeyInfo.Rows)
            {
                columns.Add($"[{row["COLUMN_NAME"].ToString()}]");
            }

            return $"CONSTRAINT [{constraintName}] PRIMARY KEY ({string.Join(", ", columns)})";
        }

        private class ForeignKeyDefinition
        {
            public string Name { get; set; }
            public string ParentSchema { get; set; }
            public string ParentTable { get; set; }
            public string ReferencedSchema { get; set; }
            public string ReferencedTable { get; set; }
            public string DeleteAction { get; set; }
            public string UpdateAction { get; set; }
            public List<KeyValuePair<string, string>> Columns { get; } = new List<KeyValuePair<string, string>>();
        }

        public static void CopyForeignKeys(string sourceConnectionString, string targetConnectionString)
        {
            using (SqlConnection sourceConnection = new SqlConnection(sourceConnectionString))
            using (SqlConnection targetConnection = new SqlConnection(targetConnectionString))
            {
                sourceConnection.Open();
                targetConnection.Open();

                ApplyForeignKeys(sourceConnection, targetConnection);
            }
        }

        private static void ApplyForeignKeys(SqlConnection sourceConnection, SqlConnection targetConnection)
        {
            const string foreignKeyQuery = @"
                SELECT
                    fk.name AS FKName,
                    schParent.name AS ParentSchema,
                    tp.name AS ParentTable,
                    schRef.name AS ReferencedSchema,
                    tr.name AS ReferencedTable,
                    cp.name AS ParentColumn,
                    cr.name AS ReferencedColumn,
                    fkc.constraint_column_id,
                    fk.delete_referential_action_desc,
                    fk.update_referential_action_desc
                FROM sys.foreign_keys AS fk
                INNER JOIN sys.foreign_key_columns AS fkc ON fk.object_id = fkc.constraint_object_id
                INNER JOIN sys.tables AS tp ON fk.parent_object_id = tp.object_id
                INNER JOIN sys.schemas AS schParent ON tp.schema_id = schParent.schema_id
                INNER JOIN sys.columns AS cp ON fkc.parent_object_id = cp.object_id AND fkc.parent_column_id = cp.column_id
                INNER JOIN sys.tables AS tr ON fk.referenced_object_id = tr.object_id
                INNER JOIN sys.schemas AS schRef ON tr.schema_id = schRef.schema_id
                INNER JOIN sys.columns AS cr ON fkc.referenced_object_id = cr.object_id AND fkc.referenced_column_id = cr.column_id
                ORDER BY fk.name, fkc.constraint_column_id";

            Dictionary<string, ForeignKeyDefinition> foreignKeys = new Dictionary<string, ForeignKeyDefinition>(StringComparer.OrdinalIgnoreCase);

            using (SqlCommand cmd = new SqlCommand(foreignKeyQuery, sourceConnection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string fkName = reader.GetString(0);
                    if (!foreignKeys.TryGetValue(fkName, out ForeignKeyDefinition definition))
                    {
                        definition = new ForeignKeyDefinition
                        {
                            Name = fkName,
                            ParentSchema = reader.GetString(1),
                            ParentTable = reader.GetString(2),
                            ReferencedSchema = reader.GetString(3),
                            ReferencedTable = reader.GetString(4),
                            DeleteAction = reader.GetString(8),
                            UpdateAction = reader.GetString(9)
                        };
                        foreignKeys.Add(fkName, definition);
                    }

                    string parentColumn = reader.GetString(5);
                    string referencedColumn = reader.GetString(6);
                    definition.Columns.Add(new KeyValuePair<string, string>(parentColumn, referencedColumn));
                }
            }

            foreach (ForeignKeyDefinition foreignKey in foreignKeys.Values)
            {
                string parentTableFullName = $"[{foreignKey.ParentSchema}].[{foreignKey.ParentTable}]";
                string referencedTableFullName = $"[{foreignKey.ReferencedSchema}].[{foreignKey.ReferencedTable}]";
                string parentColumns = string.Join(", ", foreignKey.Columns.Select(column => $"[{column.Key}]"));
                string referencedColumns = string.Join(", ", foreignKey.Columns.Select(column => $"[{column.Value}]"));

                StringBuilder fkBuilder = new StringBuilder();
                fkBuilder.Append($"ALTER TABLE {parentTableFullName} WITH NOCHECK ADD CONSTRAINT [{foreignKey.Name}] FOREIGN KEY ({parentColumns}) REFERENCES {referencedTableFullName} ({referencedColumns})");

                if (!string.Equals(foreignKey.DeleteAction, "NO_ACTION", StringComparison.OrdinalIgnoreCase))
                {
                    fkBuilder.Append($" ON DELETE {foreignKey.DeleteAction}");
                }

                if (!string.Equals(foreignKey.UpdateAction, "NO_ACTION", StringComparison.OrdinalIgnoreCase))
                {
                    fkBuilder.Append($" ON UPDATE {foreignKey.UpdateAction}");
                }

                using (SqlCommand addConstraintCmd = new SqlCommand(fkBuilder.ToString(), targetConnection))
                {
                    addConstraintCmd.ExecuteNonQuery();
                }

                string checkConstraintQuery = $"ALTER TABLE {parentTableFullName} CHECK CONSTRAINT [{foreignKey.Name}]";
                using (SqlCommand checkConstraintCmd = new SqlCommand(checkConstraintQuery, targetConnection))
                {
                    checkConstraintCmd.ExecuteNonQuery();
                }
            }
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
                    if (!string.Equals(table["TABLE_TYPE"].ToString(), "BASE TABLE", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    string tableName = table["TABLE_NAME"].ToString();
                    string tableSchema = table["TABLE_SCHEMA"].ToString();
                    string fullTableName = $"[{tableSchema}].[{tableName}]";

                    // Read data from the source table
                    string selectQuery = $"SELECT * FROM {fullTableName}";
                    using (SqlCommand selectCmd = new SqlCommand(selectQuery, sourceConn))
                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        // Get the column sizes for the target table
                        DataTable columnSizes = GetColumnSizes(targetConn, tableSchema, tableName);

                        // Insert data into the target table
                        while (reader.Read())
                        {
                            string insertQuery = $"INSERT INTO {fullTableName} VALUES (";
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object value = reader[i];

                                string columnValue;
                                if (value == DBNull.Value)
                                {
                                    columnValue = "NULL";
                                }
                                else
                                {
                                    columnValue = value.ToString();
                                }

                                // Truncate the value if it exceeds the column size
                                int columnSize = GetColumnSize(columnSizes, columnName);
                                if (columnSize > 0 && columnValue != null && columnValue != "NULL" && columnValue.Length > columnSize)
                                {
                                    columnValue = columnValue.Substring(0, columnSize);
                                }

                                // Check if the column is Unicode (NVARCHAR/NCHAR/NTEXT)
                                bool isUnicode = IsUnicodeColumn(columnSizes, columnName);

                                // Prefix Unicode strings with N''
                                if (columnValue == "NULL")
                                {
                                    insertQuery += "NULL, ";
                                }
                                else if (isUnicode)
                                {
                                    columnValue = $"N'{columnValue.Replace("'", "''")}'";
                                    insertQuery += $"{columnValue}, ";
                                }
                                else
                                {
                                    columnValue = $"'{columnValue.Replace("'", "''")}'";
                                    insertQuery += $"{columnValue}, ";
                                }
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

        public static DataTable GetColumnSizes(SqlConnection connection, string tableSchema, string tableName)
        {
            string query = @"
        SELECT COLUMN_NAME, CHARACTER_MAXIMUM_LENGTH, DATA_TYPE
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE TABLE_NAME = @TableName AND TABLE_SCHEMA = @TableSchema";
            DataTable columnSizes = new DataTable();

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@TableSchema", tableSchema);
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
