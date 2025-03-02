using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Bussiness
{
    public class GeneralRepository : IGeneralRepository
    {
        public bool ValidateMappings(List<RelationItemModels> mappings, List<ColumnInfo> destinationColumns)
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

        public List<ColumnInfo> GetColumnInfo(string connectionString, string tableName)
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

        public async Task TransferDataAsync(IProgress<int> progress, List<RelationItemModels> mappList, string oldDbConnectionString, string newDbConnectionString)
        {
            LogEvents.Log($"شروع انتقال از \n{oldDbConnectionString}\n به \n{newDbConnectionString}\n");

            int totalMappings = mappList.Count;
            int completedMappings = 0;

            foreach (var mapping in mappList)
            {
                try
                {
                    LogEvents.Log($"شروع انتقال از \n{mapping.SourceTableName}\n به \n{mapping.DestinationTableName}\n");

                    // Step 1: Retrieve column information for the destination table
                    List<ColumnInfo> destinationColumns = GetColumnInfo(newDbConnectionString, mapping.DestinationTableName);

                    // Step 2: Validate the mappings
                    if (!ValidateMappings(mappList, destinationColumns))
                    {
                        LogEvents.Log($"خطا: مقادیر مورد نیاز برای اضافه کردن اطلاعات در جدول \n'{mapping.DestinationTableName}'\n کافی نمی باشد");
                        // Optionally, stop further processing for this mapping
                        continue;
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
                                        LogEvents.Log($"اطلاعات از جدول \n'{mapping.SourceTableName}'\n به جدول \n'{mapping.DestinationTableName}'\n با موفقیت انتقال یافت.");

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
                    LogEvents.Log($"خطا در انتقال اطلاعات از جدول \n'{mapping.SourceTableName}'\n به جدول \n'{mapping.DestinationTableName}'\n: \n{ex.Message}\n");
                }
            }
            LogEvents.Log("انتقال داده ها به پایان رسید.");
        }

        public Tuple<List<string>, bool> CreateDataBase()
        {
            List<string> messages = new List<string>();
            try
            {
                return General.CreateDatabase();
            }
            catch (Exception)
            {
                return Tuple.Create(messages, false);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
