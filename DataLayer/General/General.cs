using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class General
    {
        public static Tuple<List<string>,bool> CreateDatabase()
        {
            List<string> messages = new List<string>();
            // Connection string for the master database (to check/create the database)
            string masterConnectionString = $"Server=.;Database=master;Integrated Security=True;";

            // Connection string for the target database
            string targetConnectionString = $"Server=.;Database=ConvertDB;Integrated Security=True;";

            try
            {
                // Step 1: Check if the database exists
                using (SqlConnection connection = new SqlConnection(masterConnectionString))
                {
                    connection.Open();

                    // Check if the database exists
                    string checkDatabaseQuery = $"SELECT COUNT(*) FROM sys.databases WHERE name = 'ConvertDB';";
                    using (SqlCommand command = new SqlCommand(checkDatabaseQuery, connection))
                    {
                        int databaseExists = (int)command.ExecuteScalar();

                        // If the database does not exist, create it
                        if (databaseExists == 0)
                        {
                            string createDatabaseQuery = $"CREATE DATABASE ConvertDB;";
                            using (SqlCommand createCommand = new SqlCommand(createDatabaseQuery, connection))
                            {
                                createCommand.ExecuteNonQuery();
                                messages.Add("پایگاه داده نرم افزار با موفقیت ایجاد شد");
                            }
                        }
                        else
                        {
                            messages.Add("پایگاه داده ای با نام : 'ConvertDB' موجود است");
                            Console.WriteLine($"Database 'ConvertDB' already exists.");
                        }
                    }
                }

                // Step 2: Create tables in the database
                using (SqlConnection connection = new SqlConnection(targetConnectionString))
                {
                    connection.Open();

                    // Create the [Relations] table
                    string createRelationsTableQuery = @"
                        CREATE TABLE [Relations] (
                            [RelationID] INT IDENTITY(1,1) PRIMARY KEY,
                            [SourceTableName] NVARCHAR(100) NOT NULL,
                            [SourceColumnName] NVARCHAR(100) NOT NULL,
                            [DestinationTableName] NVARCHAR(100) NOT NULL,
                            [DestinationColumnName] NVARCHAR(100) NOT NULL,
                            [EventID] INT NOT NULL
                        );";

                    using (SqlCommand command = new SqlCommand(createRelationsTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        messages.Add("جدول 'Relations' با موفقیت ایجاد شد");
                    }

                    // Create the [Events] table
                    string createEventsTableQuery = @"
                        CREATE TABLE [Events] (
                            [EventID] INT IDENTITY(1,1) PRIMARY KEY,
                            [Title] NVARCHAR(100) NOT NULL,
                            [CreateDate] DATETIME NOT NULL,
                            [Status] INT NOT NULL,
                            [DBSourceID] INT NOT NULL,
                            [DBDestinationID] INT NOT NULL
                        );";

                    using (SqlCommand command = new SqlCommand(createEventsTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        messages.Add("جدول 'Events' با موفقیت ایجاد شد");
                    }

                    // Create the [DataBases] table
                    string createDataBasesTableQuery = @"
                        CREATE TABLE [DataBases] (
                            [DBID] INT IDENTITY(1,1) PRIMARY KEY,
                            [ServerName] NVARCHAR(100) NOT NULL,
                            [DBName] NVARCHAR(100) NOT NULL,
                            [AuthTypeID] INT NOT NULL,
                            [Username] NVARCHAR(100)  NULL,
                            [Password] NVARCHAR(100)  NULL
                        );";

                    using (SqlCommand command = new SqlCommand(createDataBasesTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        messages.Add("جدول 'DataBases' با موفقیت ایجاد شد");
                    }

                    string createAuthTypesTableQuery = @"
                        CREATE TABLE [AuthTypes] (
                            [AuthTypeID] INT IDENTITY(1,1) PRIMARY KEY,
                            [Title] [nvarchar](150) NOT NULL
                        );";

                    using (SqlCommand command = new SqlCommand(createAuthTypesTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        messages.Add("جدول 'AuthTypes' با موفقیت ایجاد شد");
                    }
                }

                return Tuple.Create(messages,true);
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                messages.Add($"Error: {ex.Message}");

                // Optionally, rethrow the exception or handle it as needed
                messages.Add("ایجاد پایگاه داده نرم افزار ناموفق بود");
                return Tuple.Create(messages, false);
            }
        }
    }
}
