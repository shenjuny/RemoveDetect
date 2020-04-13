namespace RemoveDetect
{
    partial class ROIconfig
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbox = new System.Windows.Forms.PictureBox();
            this.btn_scratch = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_camera_open = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_openfile = new System.Windows.Forms.Button();
            this.image_listBox = new System.Windows.Forms.ListBox();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbox
            // 
            this.pbox.Location = new System.Drawing.Point(9, 89);
            this.pbox.Margin = new System.Windows.Forms.Padding(4);
            this.pbox.Name = "pbox";
            this.pbox.Size = new System.Drawing.Size(805, 606);
            this.pbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbox.TabIndex = 0;
            this.pbox.TabStop = false;
            this.pbox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbox_MouseDown);
            this.pbox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbox_MouseMove);
            this.pbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbox_MouseUp);
            // 
            // btn_scratch
            // 
            this.btn_scratch.Location = new System.Drawing.Point(225, 48);
            this.btn_scratch.Margin = new System.Windows.Forms.Padding(4);
            this.btn_scratch.Name = "btn_scratch";
            this.btn_scratch.Size = new System.Drawing.Size(100, 31);
            this.btn_scratch.TabIndex = 1;
            this.btn_scratch.Text = "抓图";
            this.btn_scratch.UseVisualStyleBackColor = true;
            this.btn_scratch.Click += new System.EventHandler(this.btn_scratch_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(333, 48);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(100, 31);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_camera_open
            // 
            this.btn_camera_open.Location = new System.Drawing.Point(117, 49);
            this.btn_camera_open.Margin = new System.Windows.Forms.Padding(4);
            this.btn_camera_open.Name = "btn_camera_open";
            this.btn_camera_open.Size = new System.Drawing.Size(100, 31);
            this.btn_camera_open.TabIndex = 12;
            this.btn_camera_open.Text = "实时视频";
            this.btn_camera_open.UseVisualStyleBackColor = true;
            this.btn_camera_open.Click += new System.EventHandler(this.camera_open_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(441, 49);
            this.btn_clear.Margin = new System.Windows.Forms.Padding(4);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(100, 31);
            this.btn_clear.TabIndex = 13;
            this.btn_clear.Text = "清除";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_openfile
            // 
            this.btn_openfile.Location = new System.Drawing.Point(9, 49);
            this.btn_openfile.Margin = new System.Windows.Forms.Padding(4);
            this.btn_openfile.Name = "btn_openfile";
            this.btn_openfile.Size = new System.Drawing.Size(100, 31);
            this.btn_openfile.TabIndex = 17;
            this.btn_openfile.Text = "打开文件";
            this.btn_openfile.UseVisualStyleBackColor = true;
            this.btn_openfile.Click += new System.EventHandler(this.btn_openfile_Click);
            // 
            // image_listBox
            // 
            this.image_listBox.FormattingEnabled = true;
            this.image_listBox.ItemHeight = 16;
            this.image_listBox.Location = new System.Drawing.Point(822, 339);
            this.image_listBox.Margin = new System.Windows.Forms.Padding(4);
            this.image_listBox.Name = "image_listBox";
            this.image_listBox.Size = new System.Drawing.Size(173, 356);
            this.image_listBox.TabIndex = 18;
            this.image_listBox.DoubleClick += new System.EventHandler(this.point_listBox_DoubleClick);
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.Location = new System.Drawing.Point(10, 88);
            this.videoSourcePlayer1.Margin = new System.Windows.Forms.Padding(4);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(805, 607);
            this.videoSourcePlayer1.TabIndex = 19;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(822, 88);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(169, 243);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // ROIconfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.videoSourcePlayer1);
            this.Controls.Add(this.image_listBox);
            this.Controls.Add(this.btn_openfile);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_camera_open);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_scratch);
            this.Controls.Add(this.pbox);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ROIconfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ROISelector";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbox;
        private System.Windows.Forms.Button btn_scratch;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_camera_open;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_openfile;
        private System.Windows.Forms.ListBox image_listBox;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

