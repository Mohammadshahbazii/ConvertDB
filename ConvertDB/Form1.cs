using Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConvertDB
{
    public partial class Form1 : Form
    {
        IEventsRepository eventsRepository = new EventsRepository();
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            // Wire up event handlers
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                lblConvert.Visible = true;
                pbConvert.Visible = true; 
                // Reset progress bar and start the task
                pbConvert.Value = 0;
                lblConvert.Text = "Task started...";
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            for (int i = 0; i <= 100; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                // Simulate work (e.g., data migration, file processing)
                Thread.Sleep(100); // Replace with actual work

                // Report progress
                worker.ReportProgress(i);
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Update progress bar and status label
            pbConvert.Value = e.ProgressPercentage;
            lblConvert.Text = $"{e.ProgressPercentage}%";
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                lblConvert.Text = "Task cancelled.";
            }
            else if (e.Error != null)
            {
                lblConvert.Text = $"Error: {e.Error.Message}";
            }
            else
            {
                lblConvert.Visible = false;
                pbConvert.Visible = false;
                lblConvert.Text = "Task completed!";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnConvert.Enabled = false;
            dgvConverts.DataSource = eventsRepository.GetEvents();
        }

        private void btnDBRelations_Click(object sender, EventArgs e)
        {
            frmDBRelations frmDBRelations = new frmDBRelations();
            frmDBRelations.ShowDialog();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmIntroduceDB frmIntroduceDB = new frmIntroduceDB();
            frmIntroduceDB.Text = "افزودن";
            if (frmIntroduceDB.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("عملیات با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1_Load(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmIntroduceDB frmIntroduceDB = new frmIntroduceDB();
            frmIntroduceDB.Text = "ویرایش";
            frmIntroduceDB.eventID = Convert.ToInt32(dgvConverts.CurrentRow.Cells[0].Value.ToString());
            if (frmIntroduceDB.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("عملیات با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1_Load(null, null);
            }
        }

        private void btnDefaultDBSetting_Click(object sender, EventArgs e)
        {
            frmDefaultDBSetting frmDefaultDB = new frmDefaultDBSetting();
            if (frmDefaultDB.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("عملیات با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1_Load(null, null);
            }
        }

        private void dgvConverts_SelectionChanged(object sender, EventArgs e)
        {
            if (Helpers.CanProcess(dgvConverts.CurrentRow.Cells["Status"].Value.ToString()))
            {
                btnConvert.Enabled = true;
                var id = (int)dgvConverts.CurrentRow.Cells[0].Value;
            }
            else
            {
                btnConvert.Enabled = false;
                var id = (int)dgvConverts.CurrentRow.Cells[0].Value;
            }
           
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Form1_Load(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(dgvConverts.CurrentRow.Cells[0].Value.ToString());
            var title = dgvConverts.CurrentRow.Cells["Name"].Value.ToString();
            DialogResult dr = MessageBox.Show($"آیا از حذف '{title}' اطمینان دارید؟","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes) 
            {
                if (eventsRepository.Delete(id))
                {
                    MessageBox.Show("عملیات با موفقیت انجام شد", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else 
                {
                    MessageBox.Show($"هنگام عملیات خطایی رخ داد لطفا مجددا تلاش فرمایید", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Form1_Load(null, null);
            }
        }
    }
}
