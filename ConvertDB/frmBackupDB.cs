using Bussiness;
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
    public partial class frmBackupDB : Form
    {
        public frmBackupDB()
        {
            InitializeComponent();
        }

        private void frmBackupDB_Load(object sender, EventArgs e)
        {
            cbSourceAuthorization.DataSource = Helpers.GetAuthTypes();
            cbSourceAuthorization.ValueMember = "TypeID";
            cbSourceAuthorization.DisplayMember = "Type";

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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSourceServer.Text) && !string.IsNullOrEmpty(txtSourceDBName.Text))
            {
                if (((txtSourcePassword.Enabled && txtSourceUsername.Enabled) && (string.IsNullOrEmpty(txtSourceUsername.Text) || string.IsNullOrEmpty(txtSourcePassword.Text))))
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

                    // Build the connection string
                    string sourceConnectionString = Helpers.CreateConnectionString(sourceDataBaseInfo);


                    var IsSuccess_SourceDB = Helpers.TestConnectionString(sourceConnectionString);

                    if (!IsSuccess_SourceDB)
                    {
                        if (!IsSuccess_SourceDB)
                        {
                            MessageBox.Show("مشخصات وارد شده ی پایگاه داده مبدا نامعتبر می باشد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("مشخصات وارد شده ی پایگاه داده مقصد نامعتبر می باشد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            // Set dialog properties
                            saveFileDialog.Title = "ذخیره فایل بکاپ";
                            saveFileDialog.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                            saveFileDialog.DefaultExt = "bak";
                            saveFileDialog.FileName = sourceDataBaseInfo.DataBaseName;

                            // Show the dialog and check if the user selected a path
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                // Get the file path from the dialog
                                string backupFilePath = saveFileDialog.FileName;

                                // Proceed with database backup logic
                                Helpers.BackupDatabase(backupFilePath, sourceDataBaseInfo);
                                DialogResult = DialogResult.OK;
                            }
                            else
                            {
                                MessageBox.Show($"هنگام عملیات خطایی رخ داد لطفا مجددا تلاش فرمایید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("لطفا موارد خواسته شده را وارد کنید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
