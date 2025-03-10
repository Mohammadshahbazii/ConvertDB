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
                        var serverConnectionString = Helpers.GetServerConnectionString(destinationDataBaseInfo.ServerAddress);
                        var IsSuccess_destinationeDB = Helpers.TestConnectionString(sourceConnectionString);

                        if (!IsSuccess_SourceDB)
                        {
                            MessageBox.Show("مشخصات وارد شده ی پایگاه داده مبدا نامعتبر می باشد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            try
                            {
                                Helpers.CreateDatabaseIfNotExists(serverConnectionString, destinationDataBaseInfo.DataBaseName);

                                // Step 1: Copy schema (tables and columns)
                                Helpers.CopySchema(sourceConnectionString, destinationConnectionString);

                                // Step 2: Copy data
                                Helpers.CopyData(sourceConnectionString, destinationConnectionString);
                                DialogResult = DialogResult.OK;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"هنگام عملیات خطایی رخ داد لطفا مجددا تلاش فرمایید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

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
                MessageBox.Show($"هنگام عملیات خطایی رخ داد لطفا مجددا تلاش فرمایید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
