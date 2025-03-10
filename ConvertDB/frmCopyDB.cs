using Bussiness;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace ConvertDB
{
    public partial class frmCopyDB : Form
    {
        public frmCopyDB()
        {
            InitializeComponent();
        }

        private void frmCopyDB_Load(object sender, EventArgs e)
        {
            cbSourceAuthorization.DataSource = Helpers.GetAuthTypes();
            cbSourceAuthorization.ValueMember = "TypeID";
            cbSourceAuthorization.DisplayMember = "Type";

            cbDestinationAuthorization.DataSource = Helpers.GetAuthTypes();
            cbDestinationAuthorization.ValueMember = "TypeID";
            cbDestinationAuthorization.DisplayMember = "Type";
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSourceServer.Text) && !string.IsNullOrEmpty(txtDestinationServer.Text) && !string.IsNullOrEmpty(txtSourceDBName.Text) && !string.IsNullOrEmpty(txtDestinationDB.Text))
                {
                    if (((txtSourcePassword.Enabled && txtSourceUsername.Enabled) && (string.IsNullOrEmpty(txtSourceUsername.Text) || string.IsNullOrEmpty(txtSourcePassword.Text))) ||
                        (txtDestinationPassword.Enabled && txtDestinationUsername.Enabled) && (string.IsNullOrEmpty(txtDestinationUsername.Text) || string.IsNullOrEmpty(txtDestinationPassword.Text)))
                    {
                        MessageBox.Show("لطفا موارد خواسته شده را وارد کنید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DataBaseInfo sourceDataBaseInfo = new DataBaseInfo()
                        {
                            DataBaseName = txtSourceDBName.Text,
                            Username = txtSourceUsername.Text,
                            Password = txtSourcePassword.Text,
                            ServerAddress = txtSourceServer.Text,
                            AuthenticationType = (DataBaseAuthenticationType)cbSourceAuthorization.SelectedItem
                        };

                        DataBaseInfo destinationDataBaseInfo = new DataBaseInfo()
                        {
                            DataBaseName = txtDestinationDB.Text,
                            Username = txtDestinationUsername.Text,
                            Password = txtDestinationPassword.Text,
                            ServerAddress = txtDestinationServer.Text,
                            AuthenticationType = (DataBaseAuthenticationType)cbDestinationAuthorization.SelectedItem
                        };

                        // Build the connection string
                        string sourceConnectionString = Helpers.CreateConnectionString(sourceDataBaseInfo);
                        string destinationConnectionString = Helpers.CreateConnectionString(destinationDataBaseInfo);


                        var IsSuccess_SourceDB = Helpers.TestConnectionString(sourceConnectionString);

                        if (!IsSuccess_SourceDB)
                        {
                            MessageBox.Show("مشخصات وارد شده ی پایگاه داده مبدا نامعتبر می باشد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            using (OpenFileDialog openFileDialog = new OpenFileDialog())
                            {
                                // Set dialog properties
                                openFileDialog.Title = "ذخیره فایل بکاپ";
                                openFileDialog.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                                openFileDialog.DefaultExt = "bak";
                                openFileDialog.FileName = sourceDataBaseInfo.DataBaseName;

                                // Show the dialog and check if the user selected a path
                                if (openFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    // Get the file path from the dialog
                                    string backupFilePath = openFileDialog.FileName;
                                    // Define the source database and destination database names
                                    string sourceDatabase = sourceDataBaseInfo.DataBaseName;
                                    string destinationDatabase = destinationDataBaseInfo.DataBaseName;

                                    string connectionString = $"Server={destinationDataBaseInfo.ServerAddress};Trusted_Connection=True;";

                                    // SQL script to create a copy of the database
                                    //string sqlCommand = $@"
                                    //                -- Step 1: Create the new database
                                    //                CREATE DATABASE [{destinationDatabase}];

                                    //                -- Step 2: Generate a full backup of the source database
                                    //                BACKUP DATABASE [{sourceDatabase}] 
                                    //                TO DISK = '{backupFilePath}' WITH FORMAT;

                                    //                -- Step 3: Restore the backup to the new database
                                    //                RESTORE DATABASE [{destinationDatabase}] 
                                    //                FROM DISK = '{backupFilePath}' 
                                    //                WITH MOVE '{sourceDatabase}_Data' TO 'C:\\Program Files\\Microsoft SQL Server\\MSSQL15.SQLEXPRESS\\MSSQL\\DATA\\{destinationDatabase}_Data.mdf',
                                    //                     MOVE '{sourceDatabase}_Log' TO 'C:\\Program Files\\Microsoft SQL Server\\MSSQL15.SQLEXPRESS\\MSSQL\\DATA\\{destinationDatabase}_Log.ldf';
                                    //            ";
                                    using (SqlConnection connection = new SqlConnection(sourceConnectionString))
                                    {
                                        connection.Open();

                                        // Step 1: Create the destination database if it doesn't exist
                                        string createDatabaseCommand = $@"
                                                                  IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{destinationDatabase}')
                                                                  BEGIN
                                                                      CREATE DATABASE [{destinationDatabase}];
                                                                  END";

                                        using (SqlCommand command = new SqlCommand(createDatabaseCommand, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }

                                        // Step 2: Get the list of tables from the source database
                                        string getTablesQuery = $@"
                                                                  SELECT TABLE_NAME
                                                                  FROM [{sourceDatabase}].INFORMATION_SCHEMA.TABLES
                                                                  WHERE TABLE_TYPE = 'BASE TABLE';";

                                        List<string> tableNames = new List<string>();
                                        using (SqlCommand command = new SqlCommand(getTablesQuery, connection))
                                        {
                                            using (SqlDataReader reader = command.ExecuteReader())
                                            {
                                                while (reader.Read())
                                                {
                                                    if (reader["TABLE_NAME"].ToString() != "sysdiagrams")
                                                    {
                                                        tableNames.Add(reader["TABLE_NAME"].ToString());
                                                    }
                                                }
                                            }
                                        }
                                        foreach (string tableName in tableNames)
                                        {
                                            // Copy schema (create table in destination database)
                                            string copySchemaCommand = $@"
                                                                      SELECT * INTO [{destinationDatabase}].dbo.[{tableName}]
                                                                      FROM [{sourceDatabase}].dbo.[{tableName}] WHERE 1 = 0;";

                                            using (SqlCommand command = new SqlCommand(copySchemaCommand, connection))
                                            {
                                                command.ExecuteNonQuery();
                                            }

                                            // Copy data (insert all rows into the new table)
                                            string copyDataCommand = $@"
                                                                      INSERT INTO [{destinationDatabase}].dbo.[{tableName}]
                                                                      SELECT * FROM [{sourceDatabase}].dbo.[{tableName}];";

                                            using (SqlCommand command = new SqlCommand(copyDataCommand, connection))
                                            {
                                                command.ExecuteNonQuery();
                                            }
                                        }

                                        // Step 3: Copy each table (schema and data) to the destination database

                                    }
                                    // Execute the SQL commands
                                    //using (SqlConnection connection = new SqlConnection(connectionString))
                                    //{
                                    //    SqlCommand command = new SqlCommand(sqlCommand, connection);
                                    //    connection.Open();
                                    //    command.ExecuteNonQuery();
                                    //}

                                    DialogResult = DialogResult.OK;
                                }
                                else
                                {
                                    MessageBox.Show($"هنگام عملیات خطایی رخ داد لطفا مجددا تلاش فرمایید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }



                            }


                            DialogResult = DialogResult.OK;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("لطفا موارد خواسته شده را وارد کنید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("عملیات با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbDestinationAuthorization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbDestinationAuthorization.SelectedIndex) == 1)
            {
                txtDestinationPassword.Enabled = txtDestinationUsername.Enabled = true;
            }
            else
            {
                txtDestinationPassword.Enabled = txtDestinationUsername.Enabled = false;
            }
        }

        private void cbSourceAuthorization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbSourceAuthorization.SelectedIndex) == 1)
            {
                txtSourcePassword.Enabled = txtSourceUsername.Enabled = true;
            }
            else
            {
                txtSourcePassword.Enabled = txtSourceUsername.Enabled = false;
            }
        }
    }
}
