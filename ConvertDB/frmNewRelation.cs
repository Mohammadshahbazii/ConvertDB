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
    public partial class frmNewRelation : Form
    {
        public int eventID;
        public int relationID;
        private DataBaseInfo sourceDB;
        private DataBaseInfo destinationDB;

        IDataBaseRepository dataBaseRepository = new DataBaseRepository();
        IEventsRepository eventsRepository = new EventsRepository();
        IRelationsRepository relationsRepository = new RelationsRepository();

        public frmNewRelation()
        {
            InitializeComponent();
        }

        private void frmNewRelation_Load(object sender, EventArgs e)
        {
            var eventItem = eventsRepository.GetByID(eventID);
            var sourceDB_ID = eventItem.DBSourceID;
            var destinationDB_ID = eventItem.DBDestinationID;

            sourceDB = dataBaseRepository.GetByID(sourceDB_ID);
            destinationDB = dataBaseRepository.GetByID(destinationDB_ID);


            LoadSourceTableNames(Helpers.CreateConnectionString(sourceDB));
            LoadDestinationTableNames(Helpers.CreateConnectionString(destinationDB));

            if (this.Text == "ویرایش")
            {
                var model = relationsRepository.GetById(relationID);

                cbSourceTables.SelectedItem = model.SourceTableName;
                cbSourceColumns.SelectedItem = model.SourceColumnName;
                cbDestinationTables.SelectedItem = model.DestinationTableName;
                cbDestinationColumns.SelectedItem = model.DestinationColumnName;
            }
        }

        private void LoadSourceTableNames(string connectionString)
        {
            try
            {
                // Get table names from the database
                List<string> tableNames = dataBaseRepository.GetTables(connectionString);

                // Bind table names to the first ComboBox
                cbSourceTables.DataSource = tableNames;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"هنگام دریافت جداول پایگاه داده مبدا خطایی رخ داد . لطفا مجددا تلاش فرمائید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDestinationTableNames(string connectionString)
        {
            try
            {
                // Get table names from the database
                List<string> tableNames = dataBaseRepository.GetTables(connectionString);

                // Bind table names to the first ComboBox
                cbDestinationTables.DataSource = tableNames;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"هنگام دریافت جداول پایگاه داده مقصد خطایی رخ داد . لطفا مجددا تلاش فرمائید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cbSourceTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected table name
                string selectedTable = cbSourceTables.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedTable))
                {
                    cbSourceColumns.Enabled = true;
                    // Get column names for the selected table
                    List<string> columnNames = dataBaseRepository.GetColumns(Helpers.CreateConnectionString(sourceDB), selectedTable);

                    // Bind column names to the second ComboBox
                    cbSourceColumns.DataSource = columnNames;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"هنگام دریافت ستون های جدول انتخاب شده خطایی رخ داد . لطفا مجددا تلاش فرمائید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cbDestinationTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected table name
                string selectedTable = cbDestinationTables.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedTable))
                {
                    cbDestinationColumns.Enabled = true;
                    // Get column names for the selected table
                    List<string> columnNames = dataBaseRepository.GetColumns(Helpers.CreateConnectionString(destinationDB), selectedTable);

                    // Bind column names to the second ComboBox
                    cbDestinationColumns.DataSource = columnNames;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"هنگام دریافت ستون های جدول انتخاب شده خطایی رخ داد . لطفا مجددا تلاش فرمائید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Text != "ویرایش")
            {
                RelationItemModels relation = new RelationItemModels()
                {
                    SourceTableName = cbSourceTables.SelectedItem?.ToString(),
                    SourceColumnName = cbSourceColumns.SelectedItem?.ToString(),
                    DestinationColumnName = cbDestinationColumns.SelectedItem?.ToString(),
                    DestinationTableName = cbDestinationTables.SelectedItem?.ToString(),
                    EventID = eventID,
                };
                if (relationsRepository.Create(relation))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show($"هنگام عملیات خطایی رخ داد . لطفا مجددا تلاش فرمائید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                RelationItemModels relation = new RelationItemModels()
                {
                    ID = relationID,
                    SourceTableName = cbSourceTables.SelectedItem?.ToString(),
                    SourceColumnName = cbSourceColumns.SelectedItem?.ToString(),
                    DestinationColumnName = cbDestinationColumns.SelectedItem?.ToString(),
                    DestinationTableName = cbDestinationTables.SelectedItem?.ToString(),
                    EventID = eventID,
                };
                if (relationsRepository.Update(relation))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show($"هنگام عملیات خطایی رخ داد . لطفا مجددا تلاش فرمائید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
