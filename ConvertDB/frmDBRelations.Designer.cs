namespace ConvertDB
{
    partial class frmDBRelations
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDBRelations));
            this.dgvRelations = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinationTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinationColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Event = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelations)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRelations
            // 
            this.dgvRelations.AllowUserToAddRows = false;
            this.dgvRelations.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.dgvRelations.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRelations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRelations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRelations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.SourceTableName,
            this.SourceColumnName,
            this.DestinationTableName,
            this.DestinationColumnName,
            this.Event});
            this.dgvRelations.Location = new System.Drawing.Point(12, 41);
            this.dgvRelations.Name = "dgvRelations";
            this.dgvRelations.ReadOnly = true;
            this.dgvRelations.RowHeadersWidth = 51;
            this.dgvRelations.Size = new System.Drawing.Size(566, 247);
            this.dgvRelations.TabIndex = 0;
            this.dgvRelations.SelectionChanged += new System.EventHandler(this.dgvRelations_SelectionChanged);
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
            // SourceTableName
            // 
            this.SourceTableName.DataPropertyName = "SourceTableName";
            this.SourceTableName.HeaderText = "نام جدول منبع";
            this.SourceTableName.MinimumWidth = 6;
            this.SourceTableName.Name = "SourceTableName";
            this.SourceTableName.ReadOnly = true;
            this.SourceTableName.Width = 130;
            // 
            // SourceColumnName
            // 
            this.SourceColumnName.DataPropertyName = "SourceColumnName";
            this.SourceColumnName.HeaderText = "نام ستون منبع";
            this.SourceColumnName.MinimumWidth = 6;
            this.SourceColumnName.Name = "SourceColumnName";
            this.SourceColumnName.ReadOnly = true;
            this.SourceColumnName.Width = 130;
            // 
            // DestinationTableName
            // 
            this.DestinationTableName.DataPropertyName = "DestinationTableName";
            this.DestinationTableName.HeaderText = "نام جدول مقصد";
            this.DestinationTableName.MinimumWidth = 6;
            this.DestinationTableName.Name = "DestinationTableName";
            this.DestinationTableName.ReadOnly = true;
            this.DestinationTableName.Width = 130;
            // 
            // DestinationColumnName
            // 
            this.DestinationColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DestinationColumnName.DataPropertyName = "DestinationColumnName";
            this.DestinationColumnName.HeaderText = "نام ستون مقصد";
            this.DestinationColumnName.MinimumWidth = 6;
            this.DestinationColumnName.Name = "DestinationColumnName";
            this.DestinationColumnName.ReadOnly = true;
            // 
            // Event
            // 
            this.Event.DataPropertyName = "EventID";
            this.Event.HeaderText = "EventID";
            this.Event.MinimumWidth = 6;
            this.Event.Name = "Event";
            this.Event.ReadOnly = true;
            this.Event.Visible = false;
            this.Event.Width = 125;
            // 
            // btnCreate
            // 
            this.btnCreate.Image = global::ConvertDB.Properties.Resources.icons8_plus_20;
            this.btnCreate.Location = new System.Drawing.Point(11, 7);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(30, 30);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Image = global::ConvertDB.Properties.Resources.icons8_edit_20;
            this.btnEdit.Location = new System.Drawing.Point(41, 7);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(30, 30);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = global::ConvertDB.Properties.Resources.icons8_delete_20;
            this.btnDelete.Location = new System.Drawing.Point(71, 7);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(30, 30);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(390, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "جستجو :";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(455, 14);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(122, 27);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // frmDBRelations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 300);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.dgvRelations);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDBRelations";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تعریف ارتباط پایگاه داده ها";
            this.Load += new System.EventHandler(this.frmDBRelations_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRelations;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinationTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinationColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Event;
    }
}