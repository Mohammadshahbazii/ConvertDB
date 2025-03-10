using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public static void BackupDatabase(string filePath , DataBaseInfo dataBaseInfo)
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

    }
}
