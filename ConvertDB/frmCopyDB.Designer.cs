namespace ConvertDB
{
    partial class frmCopyDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCopyDB));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDestinationAuthorization = new System.Windows.Forms.ComboBox();
            this.txtDestinationPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDestinationUsername = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDestinationDB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDestinationServer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
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
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(12, 406);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(313, 29);
            this.btnSubmit.TabIndex = 15;
            this.btnSubmit.Text = "ثبت";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDestinationAuthorization);
            this.groupBox2.Controls.Add(this.txtDestinationPassword);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtDestinationUsername);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtDestinationDB);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtDestinationServer);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(13, 210);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(312, 189);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "مشخصات پایگاه داده مقصد";
            // 
            // cbDestinationAuthorization
            // 
            this.cbDestinationAuthorization.FormattingEnabled = true;
            this.cbDestinationAuthorization.Location = new System.Drawing.Point(7, 89);
            this.cbDestinationAuthorization.Name = "cbDestinationAuthorization";
            this.cbDestinationAuthorization.Size = new System.Drawing.Size(187, 24);
            this.cbDestinationAuthorization.TabIndex = 9;
            this.cbDestinationAuthorization.SelectedIndexChanged += new System.EventHandler(this.cbDestinationAuthorization_SelectedIndexChanged);
            // 
            // txtDestinationPassword
            // 
            this.txtDestinationPassword.Enabled = false;
            this.txtDestinationPassword.Location = new System.Drawing.Point(7, 148);
            this.txtDestinationPassword.Name = "txtDestinationPassword";
            this.txtDestinationPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDestinationPassword.Size = new System.Drawing.Size(187, 23);
            this.txtDestinationPassword.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "رمز عبور :";
            // 
            // txtDestinationUsername
            // 
            this.txtDestinationUsername.Enabled = false;
            this.txtDestinationUsername.Location = new System.Drawing.Point(7, 119);
            this.txtDestinationUsername.Name = "txtDestinationUsername";
            this.txtDestinationUsername.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDestinationUsername.Size = new System.Drawing.Size(187, 23);
            this.txtDestinationUsername.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(216, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "نام کاربری :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(212, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "نوع حفاظت : ";
            // 
            // txtDestinationDB
            // 
            this.txtDestinationDB.Location = new System.Drawing.Point(7, 61);
            this.txtDestinationDB.Name = "txtDestinationDB";
            this.txtDestinationDB.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDestinationDB.Size = new System.Drawing.Size(187, 23);
            this.txtDestinationDB.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(215, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "نام پایگاه داده :";
            // 
            // txtDestinationServer
            // 
            this.txtDestinationServer.Location = new System.Drawing.Point(7, 32);
            this.txtDestinationServer.Name = "txtDestinationServer";
            this.txtDestinationServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDestinationServer.Size = new System.Drawing.Size(187, 23);
            this.txtDestinationServer.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(215, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "آدرس سرور :";
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
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "مشخصات پایگاه داده مبدا";
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
            // frmCopyDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 447);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCopyDB";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ایجاد پایگاه داده مشابه";
            this.Load += new System.EventHandler(this.frmCopyDB_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbDestinationAuthorization;
        private System.Windows.Forms.TextBox txtDestinationPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDestinationUsername;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDestinationDB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDestinationServer;
        private System.Windows.Forms.Label label10;
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
    }
}