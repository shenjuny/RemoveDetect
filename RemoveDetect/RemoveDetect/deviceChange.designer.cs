namespace RemoveDetect
{
    partial class deviceChange
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
            this.label13 = new System.Windows.Forms.Label();
            this.txtbox_note = new System.Windows.Forms.TextBox();
            this.txtbox_gate = new System.Windows.Forms.TextBox();
            this.txtbox_mask = new System.Windows.Forms.TextBox();
            this.txtbox_Num = new System.Windows.Forms.TextBox();
            this.txtbox_size = new System.Windows.Forms.TextBox();
            this.txtbox_type = new System.Windows.Forms.TextBox();
            this.txtbox_name = new System.Windows.Forms.TextBox();
            this.txtbox_IP = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cboModel = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_Num = new System.Windows.Forms.Label();
            this.lbl_size = new System.Windows.Forms.Label();
            this.lbl_type = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.lbl_IP = new System.Windows.Forms.Label();
            this.btn_cancel_device = new System.Windows.Forms.Button();
            this.btn_save_device = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(253, 167);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 16);
            this.label13.TabIndex = 39;
            this.label13.Text = "备注:";
            // 
            // txtbox_note
            // 
            this.txtbox_note.Location = new System.Drawing.Point(308, 167);
            this.txtbox_note.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbox_note.Name = "txtbox_note";
            this.txtbox_note.Size = new System.Drawing.Size(132, 26);
            this.txtbox_note.TabIndex = 38;
            // 
            // txtbox_gate
            // 
            this.txtbox_gate.Location = new System.Drawing.Point(521, 43);
            this.txtbox_gate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbox_gate.Name = "txtbox_gate";
            this.txtbox_gate.Size = new System.Drawing.Size(132, 26);
            this.txtbox_gate.TabIndex = 35;
            // 
            // txtbox_mask
            // 
            this.txtbox_mask.Location = new System.Drawing.Point(325, 43);
            this.txtbox_mask.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbox_mask.Name = "txtbox_mask";
            this.txtbox_mask.Size = new System.Drawing.Size(132, 26);
            this.txtbox_mask.TabIndex = 33;
            // 
            // txtbox_Num
            // 
            this.txtbox_Num.Location = new System.Drawing.Point(516, 104);
            this.txtbox_Num.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbox_Num.Name = "txtbox_Num";
            this.txtbox_Num.Size = new System.Drawing.Size(132, 26);
            this.txtbox_Num.TabIndex = 31;
            // 
            // txtbox_size
            // 
            this.txtbox_size.Location = new System.Drawing.Point(89, 163);
            this.txtbox_size.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbox_size.Name = "txtbox_size";
            this.txtbox_size.Size = new System.Drawing.Size(132, 26);
            this.txtbox_size.TabIndex = 29;
            // 
            // txtbox_type
            // 
            this.txtbox_type.Location = new System.Drawing.Point(320, 104);
            this.txtbox_type.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbox_type.Name = "txtbox_type";
            this.txtbox_type.Size = new System.Drawing.Size(132, 26);
            this.txtbox_type.TabIndex = 27;
            // 
            // txtbox_name
            // 
            this.txtbox_name.Location = new System.Drawing.Point(89, 104);
            this.txtbox_name.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbox_name.Name = "txtbox_name";
            this.txtbox_name.Size = new System.Drawing.Size(132, 26);
            this.txtbox_name.TabIndex = 25;
            // 
            // txtbox_IP
            // 
            this.txtbox_IP.Location = new System.Drawing.Point(95, 43);
            this.txtbox_IP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbox_IP.Name = "txtbox_IP";
            this.txtbox_IP.Size = new System.Drawing.Size(132, 26);
            this.txtbox_IP.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(461, 175);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 16);
            this.label12.TabIndex = 37;
            this.label12.Text = "使用模式";
            // 
            // cboModel
            // 
            this.cboModel.FormattingEnabled = true;
            this.cboModel.Items.AddRange(new object[] {
            "用户密码",
            "IC卡",
            "人脸识别模式",
            "组合模式"});
            this.cboModel.Location = new System.Drawing.Point(540, 172);
            this.cboModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboModel.Name = "cboModel";
            this.cboModel.Size = new System.Drawing.Size(160, 24);
            this.cboModel.TabIndex = 36;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(467, 47);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 16);
            this.label11.TabIndex = 34;
            this.label11.Text = "网关:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(239, 47);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 16);
            this.label7.TabIndex = 32;
            this.label7.Text = "子网掩码:";
            // 
            // lbl_Num
            // 
            this.lbl_Num.AutoSize = true;
            this.lbl_Num.Location = new System.Drawing.Point(461, 108);
            this.lbl_Num.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Num.Name = "lbl_Num";
            this.lbl_Num.Size = new System.Drawing.Size(48, 16);
            this.lbl_Num.TabIndex = 30;
            this.lbl_Num.Text = "编号:";
            // 
            // lbl_size
            // 
            this.lbl_size.AutoSize = true;
            this.lbl_size.Location = new System.Drawing.Point(35, 167);
            this.lbl_size.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_size.Name = "lbl_size";
            this.lbl_size.Size = new System.Drawing.Size(48, 16);
            this.lbl_size.TabIndex = 28;
            this.lbl_size.Text = "大小:";
            // 
            // lbl_type
            // 
            this.lbl_type.AutoSize = true;
            this.lbl_type.Location = new System.Drawing.Point(253, 108);
            this.lbl_type.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_type.Name = "lbl_type";
            this.lbl_type.Size = new System.Drawing.Size(48, 16);
            this.lbl_type.TabIndex = 26;
            this.lbl_type.Text = "型号:";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(35, 108);
            this.lbl_name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(48, 16);
            this.lbl_name.TabIndex = 24;
            this.lbl_name.Text = "名称:";
            // 
            // lbl_IP
            // 
            this.lbl_IP.AutoSize = true;
            this.lbl_IP.Location = new System.Drawing.Point(24, 47);
            this.lbl_IP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_IP.Name = "lbl_IP";
            this.lbl_IP.Size = new System.Drawing.Size(64, 16);
            this.lbl_IP.TabIndex = 22;
            this.lbl_IP.Text = "IP地址:";
            // 
            // btn_cancel_device
            // 
            this.btn_cancel_device.Location = new System.Drawing.Point(569, 223);
            this.btn_cancel_device.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_cancel_device.Name = "btn_cancel_device";
            this.btn_cancel_device.Size = new System.Drawing.Size(100, 31);
            this.btn_cancel_device.TabIndex = 41;
            this.btn_cancel_device.Text = "取消";
            this.btn_cancel_device.UseVisualStyleBackColor = true;
            this.btn_cancel_device.Click += new System.EventHandler(this.btn_cancel_device_Click);
            // 
            // btn_save_device
            // 
            this.btn_save_device.Location = new System.Drawing.Point(385, 223);
            this.btn_save_device.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_save_device.Name = "btn_save_device";
            this.btn_save_device.Size = new System.Drawing.Size(100, 31);
            this.btn_save_device.TabIndex = 40;
            this.btn_save_device.Text = "保存";
            this.btn_save_device.UseVisualStyleBackColor = true;
            this.btn_save_device.Click += new System.EventHandler(this.btn_save_device_Click);
            // 
            // deviceChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 284);
            this.Controls.Add(this.btn_cancel_device);
            this.Controls.Add(this.btn_save_device);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtbox_note);
            this.Controls.Add(this.txtbox_gate);
            this.Controls.Add(this.txtbox_mask);
            this.Controls.Add(this.txtbox_Num);
            this.Controls.Add(this.txtbox_size);
            this.Controls.Add(this.txtbox_type);
            this.Controls.Add(this.txtbox_name);
            this.Controls.Add(this.txtbox_IP);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cboModel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbl_Num);
            this.Controls.Add(this.lbl_size);
            this.Controls.Add(this.lbl_type);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.lbl_IP);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "deviceChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备柜编辑";
            this.Load += new System.EventHandler(this.deviceChange_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtbox_note;
        private System.Windows.Forms.TextBox txtbox_gate;
        private System.Windows.Forms.TextBox txtbox_mask;
        private System.Windows.Forms.TextBox txtbox_Num;
        private System.Windows.Forms.TextBox txtbox_size;
        private System.Windows.Forms.TextBox txtbox_type;
        private System.Windows.Forms.TextBox txtbox_name;
        private System.Windows.Forms.TextBox txtbox_IP;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboModel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_Num;
        private System.Windows.Forms.Label lbl_size;
        private System.Windows.Forms.Label lbl_type;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.Label lbl_IP;
        private System.Windows.Forms.Button btn_cancel_device;
        private System.Windows.Forms.Button btn_save_device;
    }
}