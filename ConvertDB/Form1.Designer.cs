namespace ConvertDB
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dgvConverts = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConvertName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.pbConvert = new System.Windows.Forms.ProgressBar();
            this.lblConvert = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConverts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConverts
            // 
            this.dgvConverts.AllowUserToAddRows = false;
            this.dgvConverts.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConverts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvConverts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConverts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ConvertName,
            this.CreateDate,
            this.Status});
            this.dgvConverts.Location = new System.Drawing.Point(12, 41);
            this.dgvConverts.Name = "dgvConverts";
            this.dgvConverts.ReadOnly = true;
            this.dgvConverts.Size = new System.Drawing.Size(398, 183);
            this.dgvConverts.TabIndex = 1;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // ConvertName
            // 
            this.ConvertName.DataPropertyName = "ConvertName";
            this.ConvertName.HeaderText = "نام عملیات";
            this.ConvertName.Name = "ConvertName";
            this.ConvertName.ReadOnly = true;
            this.ConvertName.Width = 120;
            // 
            // CreateDate
            // 
            this.CreateDate.HeaderText = "تاریخ";
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.ReadOnly = true;
            this.CreateDate.Width = 80;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Status.HeaderText = "وضعیت";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Image = global::ConvertDB.Properties.Resources.icons8_edit_20;
            this.button1.Location = new System.Drawing.Point(42, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            this.btnCreate.Image = global::ConvertDB.Properties.Resources.icons8_plus_20;
            this.btnCreate.Location = new System.Drawing.Point(12, 9);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(30, 30);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // pbConvert
            // 
            this.pbConvert.Location = new System.Drawing.Point(108, 9);
            this.pbConvert.Name = "pbConvert";
            this.pbConvert.Size = new System.Drawing.Size(266, 30);
            this.pbConvert.TabIndex = 7;
            this.pbConvert.Visible = false;
            // 
            // lblConvert
            // 
            this.lblConvert.AutoSize = true;
            this.lblConvert.BackColor = System.Drawing.SystemColors.Control;
            this.lblConvert.Location = new System.Drawing.Point(222, 16);
            this.lblConvert.Name = "lblConvert";
            this.lblConvert.Size = new System.Drawing.Size(41, 16);
            this.lblConvert.TabIndex = 8;
            this.lblConvert.Text = "label1";
            this.lblConvert.Visible = false;
            // 
            // button2
            // 
            this.button2.Image = global::ConvertDB.Properties.Resources.icons8_delete_20;
            this.button2.Location = new System.Drawing.Point(72, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 6;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnConvert
            // 
            this.btnConvert.Image = global::ConvertDB.Properties.Resources.icons8_progress_25;
            this.btnConvert.Location = new System.Drawing.Point(380, 9);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(30, 30);
            this.btnConvert.TabIndex = 9;
            this.btnConvert.Tag = "";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 236);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.lblConvert);
            this.Controls.Add(this.pbConvert);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.dgvConverts);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "انتقال اطلاعات پایگاه داده";
            ((System.ComponentModel.ISupportInitialize)(this.dgvConverts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvConverts;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConvertName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ProgressBar pbConvert;
        private System.Windows.Forms.Label lblConvert;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnConvert;
    }
}

