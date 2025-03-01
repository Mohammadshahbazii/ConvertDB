using Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertDB
{
    public partial class frmDBRelations : Form
    {
        public int eventID;
        private int sourceDB_ID;
        private int destinationDB_ID;

        IDataBaseRepository dataBaseRepository = new DataBaseRepository();
        IEventsRepository eventsRepository = new EventsRepository();
        IRelationsRepository relationsRepository = new RelationsRepository();

        public frmDBRelations()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmNewRelation newRelation = new frmNewRelation();
            newRelation.eventID = eventID;
            if (newRelation.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("عملیات با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGrid();
            }
        }

        private void BindGrid()
        {
            dgvRelations.DataSource = relationsRepository.GetRelationsByEventID(eventID);
        }

        private void frmDBRelations_Load(object sender, EventArgs e)
        {
            var eventItem = eventsRepository.GetByID(eventID);
            sourceDB_ID = eventItem.DBSourceID;
            destinationDB_ID = eventItem.DBDestinationID;

            BindGrid();
        }
    }
}
