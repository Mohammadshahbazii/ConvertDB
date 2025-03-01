using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace ConvertDB
{
    public partial class frmDefaultDBSetting : Form
    {
        public frmDefaultDBSetting()
        {
            InitializeComponent();
        }

        private void frmDefaultDBSetting_Load(object sender, EventArgs e)
        {

            cbAuthorization.DataSource = Helpers.GetAuthTypes();
            cbAuthorization.ValueMember = "TypeID";
            cbAuthorization.DisplayMember = "Type";

            string connectionString = Helpers.GetDefaultConnectionString();
            var dataBaseInfo = Helpers.ParseConnectionString(connectionString);
            txtServer.Text = dataBaseInfo.ServerAddress;
            txtDBName.Text = dataBaseInfo.DataBaseName;

            if (!string.IsNullOrEmpty(dataBaseInfo.Username) && !string.IsNullOrEmpty(dataBaseInfo.Password))
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;

                cbAuthorization.SelectedIndex = 1;

                txtUsername.Text = dataBaseInfo.Username;
                txtPassword.Text = dataBaseInfo.Password;
            }
        }

        private void cbAuthorization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbAuthorization.SelectedIndex) == 1)
            {
                txtPassword.Enabled = txtUsername.Enabled = true;
            }
            else
            {
                txtPassword.Enabled = txtUsername.Enabled = false;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtServer.Text) && !string.IsNullOrEmpty(txtDBName.Text))
                {
                    if ((txtPassword.Enabled && txtUsername.Enabled) && (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text)))
                    {
                        MessageBox.Show("لطفا موارد خواسته شده را وارد کنید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DataBaseInfo dataBaseInfo = new DataBaseInfo()
                        {
                            DataBaseName = txtDBName.Text,
                            Username = txtUsername.Text,
                            Password = txtPassword.Text,
                            ServerAddress = txtServer.Text,
                            AuthenticationType = (DataBaseAuthenticationType)cbAuthorization.SelectedItem
                        };

                        // Build the connection string
                        string connectionString = Helpers.CreateConnectionString(dataBaseInfo);

                        if (Helpers.TestConnectionString(connectionString))
                        {
                            // Save the connection string to app.config
                            Helpers.SaveConnectionStringToConfig("DefaultDatabase", connectionString);

                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("مشخصات وارد شده نامعتبر می باشد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                   
                }
                else
                {
                    MessageBox.Show("لطفا موارد خواسته شده را وارد کنید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"هنگام عملیات خطایی رخ داد لطفا مجددا تلاش فرمایید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
