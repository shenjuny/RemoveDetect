namespace RemoveDetect
{
    partial class toolChange
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
            this.txtToolPic = new System.Windows.Forms.TextBox();
            this.txtToolAddress = new System.Windows.Forms.TextBox();
            this.txtToolName = new System.Windows.Forms.TextBox();
            this.txtToolNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_cancel_device = new System.Windows.Forms.Button();
            this.btn_save_device = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtToolPic
            // 
            this.txtToolPic.Location = new System.Drawing.Point(361, 108);
            this.txtToolPic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtToolPic.Name = "txtToolPic";
            this.txtToolPic.Size = new System.Drawing.Size(132, 26);
            this.txtToolPic.TabIndex = 45;
            // 
            // txtToolAddress
            // 
            this.txtToolAddress.Location = new System.Drawing.Point(127, 108);
            this.txtToolAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtToolAddress.Name = "txtToolAddress";
            this.txtToolAddress.Size = new System.Drawing.Size(132, 26);
            this.txtToolAddress.TabIndex = 43;
            // 
            // txtToolName
            // 
            this.txtToolName.Location = new System.Drawing.Point(361, 48);
            this.txtToolName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtToolName.Name = "txtToolName";
            this.txtToolName.Size = new System.Drawing.Size(132, 26);
            this.txtToolName.TabIndex = 41;
            // 
            // txtToolNo
            // 
            this.txtToolNo.Location = new System.Drawing.Point(127, 48);
            this.txtToolNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtToolNo.Name = "txtToolNo";
            this.txtToolNo.Size = new System.Drawing.Size(132, 26);
            this.txtToolNo.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(281, 112);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 44;
            this.label6.Text = "物品图片:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 112);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 42;
            this.label8.Text = "存储位置:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(281, 52);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 40;
            this.label9.Text = "物品名称:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(68, 55);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 16);
            this.label10.TabIndex = 38;
            this.label10.Text = "编号:";
            // 
            // btn_cancel_device
            // 
            this.btn_cancel_device.Location = new System.Drawing.Point(344, 173);
            this.btn_cancel_device.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_cancel_device.Name = "btn_cancel_device";
            this.btn_cancel_device.Size = new System.Drawing.Size(100, 31);
            this.btn_cancel_device.TabIndex = 47;
            this.btn_cancel_device.Text = "取消";
            this.btn_cancel_device.UseVisualStyleBackColor = true;
            this.btn_cancel_device.Click += new System.EventHandler(this.btn_cancel_device_Click);
            // 
            // btn_save_device
            // 
            this.btn_save_device.Location = new System.Drawing.Point(216, 173);
            this.btn_save_device.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_save_device.Name = "btn_save_device";
            this.btn_save_device.Size = new System.Drawing.Size(100, 31);
            this.btn_save_device.TabIndex = 46;
            this.btn_save_device.Text = "保存";
            this.btn_save_device.UseVisualStyleBackColor = true;
            this.btn_save_device.Click += new System.EventHandler(this.btn_save_device_Click);
            // 
            // toolChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 259);
            this.Controls.Add(this.btn_cancel_device);
            this.Controls.Add(this.btn_save_device);
            this.Controls.Add(this.txtToolPic);
            this.Controls.Add(this.txtToolAddress);
            this.Controls.Add(this.txtToolName);
            this.Controls.Add(this.txtToolNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "toolChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工具编辑";
            this.Load += new System.EventHandler(this.toolChange_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtToolPic;
        private System.Windows.Forms.TextBox txtToolAddress;
        private System.Windows.Forms.TextBox txtToolName;
        private System.Windows.Forms.TextBox txtToolNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_cancel_device;
        private System.Windows.Forms.Button btn_save_device;
    }
}