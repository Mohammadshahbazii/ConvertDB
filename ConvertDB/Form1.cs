using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConvertDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            pbConvert.Visible = lblConvert.Visible= true;
            lblConvert.Text = "";
            pbConvert.Value = 0;

            // Simulate a process by incrementing the progress bar's value.
            for (int i = 0; i <= pbConvert.Maximum; i++)
            {
                // Perform some task here.
                System.Threading.Thread.Sleep(10); // Simulate a time-consuming task.

                // Update the progress bar's value.
                pbConvert.Value = i;
                lblConvert.Text = i + " % ";
                pbConvert.Refresh();
            }
            lblConvert.Text = "انجام شد";
        }
    }
}
