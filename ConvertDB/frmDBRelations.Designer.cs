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
            this.btnCreate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SourceTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceColumns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinationTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinationColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRelations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRelations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRelations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SourceTableName,
            this.SourceColumns,
            this.DestinationTableName,
            this.DestinationColumnName});
            this.dgvRelations.Location = new System.Drawing.Point(12, 41);
            this.dgvRelations.Name = "dgvRelations";
            this.dgvRelations.ReadOnly = true;
            this.dgvRelations.Size = new System.Drawing.Size(566, 247);
            this.dgvRelations.TabIndex = 0;
            // 
            // btnCreate
            // 
            this.btnCreate.Image = global::ConvertDB.Properties.Resources.icons8_plus_20;
            this.btnCreate.Location = new System.Drawing.Point(11, 7);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(30, 30);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Image = global::ConvertDB.Properties.Resources.icons8_edit_20;
            this.button1.Location = new System.Drawing.Point(41, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Image = global::ConvertDB.Properties.Resources.icons8_delete_20;
            this.button2.Location = new System.Drawing.Point(71, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(390, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "جستجو :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(455, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(122, 23);
            this.textBox1.TabIndex = 5;
            // 
            // SourceTableName
            // 
            this.SourceTableName.HeaderText = "نام جدول منبع";
            this.SourceTableName.Name = "SourceTableName";
            this.SourceTableName.ReadOnly = true;
            this.SourceTableName.Width = 130;
            // 
            // SourceColumns
            // 
            this.SourceColumns.HeaderText = "نام ستون منبع";
            this.SourceColumns.Name = "SourceColumns";
            this.SourceColumns.ReadOnly = true;
            this.SourceColumns.Width = 130;
            // 
            // DestinationTableName
            // 
            this.DestinationTableName.HeaderText = "نام جدول مقصد";
            this.DestinationTableName.Name = "DestinationTableName";
            this.DestinationTableName.ReadOnly = true;
            this.DestinationTableName.Width = 130;
            // 
            // DestinationColumnName
            // 
            this.DestinationColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DestinationColumnName.HeaderText = "نام ستون مقصد";
            this.DestinationColumnName.Name = "DestinationColumnName";
            this.DestinationColumnName.ReadOnly = true;
            // 
            // frmDBRelations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 300);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvRelations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRelations;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceColumns;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinationTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinationColumnName;
    }
}