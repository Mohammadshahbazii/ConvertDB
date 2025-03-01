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
            var list = relationsRepository.GetRelationsByEventID(eventID);

            dgvRelations.DataSource = list;

            if (list.Any())
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                txtSearch.Enabled = true;
            }
            else 
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                txtSearch.Enabled = false;

            }
        }

        private void frmDBRelations_Load(object sender, EventArgs e)
        {
            var eventItem = eventsRepository.GetByID(eventID);
            sourceDB_ID = eventItem.DBSourceID;
            destinationDB_ID = eventItem.DBDestinationID;

            BindGrid();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmNewRelation newRelation = new frmNewRelation();
            newRelation.relationID = Convert.ToInt32(dgvRelations.CurrentRow.Cells[0].Value.ToString());
            newRelation.eventID = eventID;
            newRelation.Text = "ویرایش";
            if (newRelation.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("عملیات با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var relationID = Convert.ToInt32(dgvRelations.CurrentRow.Cells[0].Value.ToString());
            DialogResult dr = MessageBox.Show("آیا از حذف ردیف انتخاب شده اطمینان دارید ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) 
            {
                if (relationsRepository.Delete(relationID))
                {
                    MessageBox.Show("عملیات با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGrid();
                }
                else
                {
                    MessageBox.Show($"هنگام عملیات خطایی رخ داد لطفا مجددا تلاش فرمایید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvRelations.DataSource = relationsRepository.Search(txtSearch.Text);
        }

        private void dgvRelations_SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
        }
    }
}
