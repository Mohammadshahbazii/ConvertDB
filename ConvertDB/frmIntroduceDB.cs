using Bussiness;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace ConvertDB
{
    public partial class frmIntroduceDB : Form
    {
        public int eventID;
        IDataBaseRepository dataBaseRepository = new DataBaseRepository();
        IEventsRepository eventsRepository = new EventsRepository();
        public frmIntroduceDB()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Text == "افزودن")
            {
                if (!string.IsNullOrEmpty(txtTitle.Text))
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
                            var IsSuccess_DestinationDB = Helpers.TestConnectionString(destinationConnectionString);

                            if (!IsSuccess_SourceDB || !IsSuccess_DestinationDB)
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
                                int sourceDB_ID = dataBaseRepository.Create(sourceDataBaseInfo);
                                int destinationDB_ID = dataBaseRepository.Create(destinationDataBaseInfo);

                                if (sourceDB_ID == 0 || destinationDB_ID == 0)
                                {
                                    if (sourceDB_ID == 0)
                                    {
                                        MessageBox.Show("هنگام ایجاد مشخصات پایگاه داده مبدا خطایی رخ داد . لطفا مجددا تلاش فرمائید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        MessageBox.Show("هنگام ایجاد مشخصات پایگاه داده مقصد خطایی رخ داد . لطفا مجددا تلاش فرمائید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    EventsItemModels eventItem = new EventsItemModels()
                                    {
                                        DBDestinationID = destinationDB_ID,
                                        DBSourceID = sourceDB_ID,
                                        CreateDate = DateTime.Now,
                                        Name = txtTitle.Text,
                                        Status = 0
                                    };

                                    if (!eventsRepository.Create(eventItem))
                                    {
                                        MessageBox.Show($"هنگام عملیات خطایی رخ داد لطفا مجددا تلاش فرمایید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        DialogResult = DialogResult.OK;
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
                else
                {
                    MessageBox.Show("لطفا نام عملیات را وارد نمایید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {

            }
        }

        private void frmIntroduceDB_Load(object sender, EventArgs e)
        {
            cbSourceAuthorization.DataSource = Helpers.GetAuthTypes();
            cbSourceAuthorization.ValueMember = "TypeID";
            cbSourceAuthorization.DisplayMember = "Type";

            cbDestinationAuthorization.DataSource = Helpers.GetAuthTypes();
            cbDestinationAuthorization.ValueMember = "TypeID";
            cbDestinationAuthorization.DisplayMember = "Type";

            if (this.Text == "ویرایش")
            {
                var eventItem = eventsRepository.GetByID(eventID);
                var sourceDB = dataBaseRepository.GetByID(eventItem.DBSourceID);
                var destinationDB = dataBaseRepository.GetByID(eventItem.DBDestinationID);
                txtTitle.Text = eventItem.Name;

                txtSourceServer.Text = sourceDB.ServerAddress;
                txtSourceDBName.Text = sourceDB.DataBaseName;

                if (!string.IsNullOrEmpty(sourceDB.Username) && !string.IsNullOrEmpty(sourceDB.Password))
                {
                    txtSourceUsername.Enabled = true;
                    txtSourcePassword.Enabled = true;

                    cbSourceAuthorization.SelectedIndex = 1;

                    txtSourceUsername.Text = sourceDB.Username;
                    txtSourcePassword.Text = sourceDB.Password;
                }
                

                txtDestinationServer.Text = destinationDB.ServerAddress;
                txtDestinationDB.Text = destinationDB.DataBaseName;

                if (!string.IsNullOrEmpty(destinationDB.Username) && !string.IsNullOrEmpty(destinationDB.Password))
                {
                    txtDestinationUsername.Enabled = true;
                    txtDestinationPassword.Enabled = true;

                    cbSourceAuthorization.SelectedIndex = 1;

                    txtDestinationUsername.Text = destinationDB.Username;
                    txtDestinationPassword.Text = destinationDB.Password;
                }
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
    }
}
