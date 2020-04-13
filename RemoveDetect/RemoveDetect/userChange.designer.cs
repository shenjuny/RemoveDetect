namespace RemoveDetect
{
    partial class userChange
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
            this.txtUserFace = new System.Windows.Forms.TextBox();
            this.txtUserIC = new System.Windows.Forms.TextBox();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_cancel_device = new System.Windows.Forms.Button();
            this.btn_save_device = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUserFace
            // 
            this.txtUserFace.Location = new System.Drawing.Point(360, 113);
            this.txtUserFace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUserFace.Name = "txtUserFace";
            this.txtUserFace.Size = new System.Drawing.Size(132, 26);
            this.txtUserFace.TabIndex = 34;
            // 
            // txtUserIC
            // 
            this.txtUserIC.Location = new System.Drawing.Point(115, 113);
            this.txtUserIC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUserIC.Name = "txtUserIC";
            this.txtUserIC.Size = new System.Drawing.Size(132, 26);
            this.txtUserIC.TabIndex = 32;
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Location = new System.Drawing.Point(360, 55);
            this.txtUserPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Size = new System.Drawing.Size(132, 26);
            this.txtUserPassword.TabIndex = 30;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(119, 55);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(132, 26);
            this.txtUserName.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 117);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 33;
            this.label2.Text = "人脸登记:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 117);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 31;
            this.label3.Text = "IC卡:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(273, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 29;
            this.label4.Text = "密码:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 61);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 27;
            this.label5.Text = "用户名:";
            // 
            // btn_cancel_device
            // 
            this.btn_cancel_device.Location = new System.Drawing.Point(332, 188);
            this.btn_cancel_device.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_cancel_device.Name = "btn_cancel_device";
            this.btn_cancel_device.Size = new System.Drawing.Size(100, 31);
            this.btn_cancel_device.TabIndex = 43;
            this.btn_cancel_device.Text = "取消";
            this.btn_cancel_device.UseVisualStyleBackColor = true;
            this.btn_cancel_device.Click += new System.EventHandler(this.btn_cancel_device_Click);
            // 
            // btn_save_device
            // 
            this.btn_save_device.Location = new System.Drawing.Point(148, 188);
            this.btn_save_device.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_save_device.Name = "btn_save_device";
            this.btn_save_device.Size = new System.Drawing.Size(100, 31);
            this.btn_save_device.TabIndex = 42;
            this.btn_save_device.Text = "保存";
            this.btn_save_device.UseVisualStyleBackColor = true;
            this.btn_save_device.Click += new System.EventHandler(this.btn_save_device_Click);
            // 
            // userChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 261);
            this.Controls.Add(this.btn_cancel_device);
            this.Controls.Add(this.btn_save_device);
            this.Controls.Add(this.txtUserFace);
            this.Controls.Add(this.txtUserIC);
            this.Controls.Add(this.txtUserPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "userChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户编辑";
            this.Load += new System.EventHandler(this.userChange_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtUserFace;
        private System.Windows.Forms.TextBox txtUserIC;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_cancel_device;
        private System.Windows.Forms.Button btn_save_device;
    }
}