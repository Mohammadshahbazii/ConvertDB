namespace ConvertDB
{
    partial class frmBackupDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackupDB));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSourceAuthorization = new System.Windows.Forms.ComboBox();
            this.txtSourcePassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSourceUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSourceDBName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSourceServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSourceAuthorization);
            this.groupBox1.Controls.Add(this.txtSourcePassword);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSourceUsername);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtSourceDBName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSourceServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(312, 189);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "مشخصات پایگاه داده";
            // 
            // cbSourceAuthorization
            // 
            this.cbSourceAuthorization.FormattingEnabled = true;
            this.cbSourceAuthorization.Location = new System.Drawing.Point(7, 89);
            this.cbSourceAuthorization.Name = "cbSourceAuthorization";
            this.cbSourceAuthorization.Size = new System.Drawing.Size(187, 24);
            this.cbSourceAuthorization.TabIndex = 4;
            this.cbSourceAuthorization.SelectedIndexChanged += new System.EventHandler(this.cbSourceAuthorization_SelectedIndexChanged);
            // 
            // txtSourcePassword
            // 
            this.txtSourcePassword.Enabled = false;
            this.txtSourcePassword.Location = new System.Drawing.Point(7, 148);
            this.txtSourcePassword.Name = "txtSourcePassword";
            this.txtSourcePassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSourcePassword.Size = new System.Drawing.Size(187, 23);
            this.txtSourcePassword.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "رمز عبور :";
            // 
            // txtSourceUsername
            // 
            this.txtSourceUsername.Enabled = false;
            this.txtSourceUsername.Location = new System.Drawing.Point(7, 119);
            this.txtSourceUsername.Name = "txtSourceUsername";
            this.txtSourceUsername.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSourceUsername.Size = new System.Drawing.Size(187, 23);
            this.txtSourceUsername.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "نام کاربری :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "نوع حفاظت : ";
            // 
            // txtSourceDBName
            // 
            this.txtSourceDBName.Location = new System.Drawing.Point(7, 61);
            this.txtSourceDBName.Name = "txtSourceDBName";
            this.txtSourceDBName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSourceDBName.Size = new System.Drawing.Size(187, 23);
            this.txtSourceDBName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "نام پایگاه داده :";
            // 
            // txtSourceServer
            // 
            this.txtSourceServer.Location = new System.Drawing.Point(7, 32);
            this.txtSourceServer.Name = "txtSourceServer";
            this.txtSourceServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtSourceServer.Size = new System.Drawing.Size(187, 23);
            this.txtSourceServer.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "آدرس سرور :";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(13, 209);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(313, 29);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "ثبت";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmBackupDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 246);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBackupDB";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تهیه بکاپ از پایگاه داده";
            this.Load += new System.EventHandler(this.frmBackupDB_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbSourceAuthorization;
        private System.Windows.Forms.TextBox txtSourcePassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSourceUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSourceDBName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSourceServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmit;
    }
}