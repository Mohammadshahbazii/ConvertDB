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
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.pbConvert = new System.Windows.Forms.ProgressBar();
            this.lblConvert = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.تنظیماتToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCreateDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDefaultDBSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDBRelations = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pbWelcome = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConverts)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWelcome)).BeginInit();
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
            this.Name,
            this.CreateDate,
            this.Status});
            this.dgvConverts.Location = new System.Drawing.Point(12, 63);
            this.dgvConverts.Name = "dgvConverts";
            this.dgvConverts.ReadOnly = true;
            this.dgvConverts.RowHeadersWidth = 51;
            this.dgvConverts.Size = new System.Drawing.Size(398, 183);
            this.dgvConverts.TabIndex = 1;
            this.dgvConverts.SelectionChanged += new System.EventHandler(this.dgvConverts_SelectionChanged);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 125;
            // 
            // Name
            // 
            this.Name.DataPropertyName = "Name";
            this.Name.HeaderText = "نام عملیات";
            this.Name.MinimumWidth = 6;
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            this.Name.Width = 120;
            // 
            // CreateDate
            // 
            this.CreateDate.DataPropertyName = "CreateDate";
            this.CreateDate.HeaderText = "تاریخ";
            this.CreateDate.MinimumWidth = 6;
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.ReadOnly = true;
            this.CreateDate.Width = 140;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "وضعیت";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::ConvertDB.Properties.Resources.icons8_edit_20;
            this.btnEdit.Location = new System.Drawing.Point(42, 31);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(30, 30);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Image = global::ConvertDB.Properties.Resources.icons8_plus_20;
            this.btnCreate.Location = new System.Drawing.Point(12, 31);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(30, 30);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // pbConvert
            // 
            this.pbConvert.Location = new System.Drawing.Point(108, 31);
            this.pbConvert.Name = "pbConvert";
            this.pbConvert.Size = new System.Drawing.Size(266, 30);
            this.pbConvert.TabIndex = 7;
            this.pbConvert.Visible = false;
            // 
            // lblConvert
            // 
            this.lblConvert.AutoSize = true;
            this.lblConvert.BackColor = System.Drawing.SystemColors.Control;
            this.lblConvert.Location = new System.Drawing.Point(222, 38);
            this.lblConvert.Name = "lblConvert";
            this.lblConvert.Size = new System.Drawing.Size(54, 21);
            this.lblConvert.TabIndex = 8;
            this.lblConvert.Text = "label1";
            this.lblConvert.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::ConvertDB.Properties.Resources.icons8_delete_20;
            this.btnDelete.Location = new System.Drawing.Point(72, 31);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(30, 30);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Image = global::ConvertDB.Properties.Resources.icons8_progress_25;
            this.btnConvert.Location = new System.Drawing.Point(380, 31);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(30, 30);
            this.btnConvert.TabIndex = 9;
            this.btnConvert.Tag = "";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.تنظیماتToolStripMenuItem,
            this.btnDBRelations,
            this.btnRefresh});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(422, 28);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // تنظیماتToolStripMenuItem
            // 
            this.تنظیماتToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCreateDataBase,
            this.toolStripSeparator1,
            this.btnDefaultDBSetting});
            this.تنظیماتToolStripMenuItem.Image = global::ConvertDB.Properties.Resources.icons8_settings_25;
            this.تنظیماتToolStripMenuItem.Name = "تنظیماتToolStripMenuItem";
            this.تنظیماتToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.تنظیماتToolStripMenuItem.Text = "تنظیمات";
            this.تنظیماتToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // btnCreateDataBase
            // 
            this.btnCreateDataBase.Name = "btnCreateDataBase";
            this.btnCreateDataBase.Size = new System.Drawing.Size(246, 26);
            this.btnCreateDataBase.Text = "ایجاد پایگاه داده نرم افزار";
            this.btnCreateDataBase.Click += new System.EventHandler(this.btnCreateDataBase_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(243, 6);
            // 
            // btnDefaultDBSetting
            // 
            this.btnDefaultDBSetting.Name = "btnDefaultDBSetting";
            this.btnDefaultDBSetting.Size = new System.Drawing.Size(246, 26);
            this.btnDefaultDBSetting.Text = "پایگاه داده نرم افزار";
            this.btnDefaultDBSetting.Click += new System.EventHandler(this.btnDefaultDBSetting_Click);
            // 
            // btnDBRelations
            // 
            this.btnDBRelations.Image = global::ConvertDB.Properties.Resources.icons8_table_94;
            this.btnDBRelations.Name = "btnDBRelations";
            this.btnDBRelations.Size = new System.Drawing.Size(212, 24);
            this.btnDBRelations.Text = "تعریف ارتباط پایگاه داده ها";
            this.btnDBRelations.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnDBRelations.Click += new System.EventHandler(this.btnDBRelations_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::ConvertDB.Properties.Resources.icons8_refresh_25;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(106, 24);
            this.btnRefresh.Text = "بروزرسانی";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblWelcome.Location = new System.Drawing.Point(19, 168);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(28, 16);
            this.lblWelcome.TabIndex = 11;
            this.lblWelcome.Text = "test";
            // 
            // pbWelcome
            // 
            this.pbWelcome.Image = global::ConvertDB.Properties.Resources.icons8_database_100__1_;
            this.pbWelcome.Location = new System.Drawing.Point(129, 31);
            this.pbWelcome.Name = "pbWelcome";
            this.pbWelcome.Size = new System.Drawing.Size(173, 121);
            this.pbWelcome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbWelcome.TabIndex = 12;
            this.pbWelcome.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 258);
            this.Controls.Add(this.pbWelcome);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.lblConvert);
            this.Controls.Add(this.pbConvert);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.dgvConverts);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "انتقال اطلاعات پایگاه داده";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConverts)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWelcome)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvConverts;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ProgressBar pbConvert;
        private System.Windows.Forms.Label lblConvert;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnConvert;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnDBRelations;
        private System.Windows.Forms.ToolStripMenuItem btnRefresh;
        private System.Windows.Forms.ToolStripMenuItem تنظیماتToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnDefaultDBSetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.ToolStripMenuItem btnCreateDataBase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.PictureBox pbWelcome;
    }
}

