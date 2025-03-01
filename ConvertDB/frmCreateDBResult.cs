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
    public partial class frmCreateDBResult : Form
    {
        IGeneralRepository generalRepository = new GeneralRepository();

        private string connectionString;
        public frmCreateDBResult()
        {
            InitializeComponent();
        }

        private void frmCreateDBResult_Load(object sender, EventArgs e)
        {
            // Example: Create a List<string>
            Tuple<List<string>,bool> data = generalRepository.CreateDataBase();

            // Convert the List<string> to a DataTable
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("نتایج", typeof(string)); // Add a column with a custom header

            foreach (var item in data.Item1)
            {
                dataTable.Rows.Add(item); // Add each string as a row
            }

            // Bind the DataTable to the DataGridView
            dgvResults.DataSource = dataTable;

            if (data.Item2)
            {
                DataBaseInfo dataBaseInfo = new DataBaseInfo()
                {
                    DataBaseName = "ConvertDB",
                    ServerAddress = ".",
                    AuthenticationType = new DataBaseAuthenticationType() { TypeID = 1, Type = "Windows Authentication" }
                };

                // Build the connection string
                connectionString = Helpers.CreateConnectionString(dataBaseInfo);
                btnSubmit.Enabled = true;
            }

            // Customize the DataGridView
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Adjust column width
            dgvResults.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align headers

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Helpers.TestConnectionString(connectionString))
            {
                // Save the connection string to app.config
                Helpers.SaveConnectionStringToConfig("DefaultDatabase", connectionString);

            }
            MessageBox.Show("نرم افزار با موفقیت به پایگاه داده متصل شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }
    }
}
