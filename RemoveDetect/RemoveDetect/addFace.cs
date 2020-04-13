using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using System.IO;

namespace RemoveDetect
{
    public delegate void setTextValue(string textValue);
    public partial class addFace : Form
    {
        public event setTextValue setFormTextValue;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private int Indexof = 0;
        public bool isok = false;
        Bitmap bitmap;
        public string img_path = "";
        public string user_num= "";
        public addFace(string user_ID)
        {
            InitializeComponent();
            user_num = user_ID;
            
        }
        //添加所有的摄像头到combobox列表里
        private void Camlist()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
            {
                MessageBox.Show("未找到摄像头设备");
            }
            foreach (FilterInfo device in videoDevices)
            {
                Cameralist.Items.Add(device.Name);
            }
        }

        private void checkDevice_Click(object sender, EventArgs e)
        { // 加载出来所有的摄像头
            Camlist();
        }
        //选择要调用的摄像头，捕获视频并展示到videoSourcePlayer1
        private void openDevice_Click(object sender, EventArgs e)
        {
            Indexof = Cameralist.SelectedIndex;
            if (Indexof < 0)
            {
                MessageBox.Show("请选择一个摄像头");
                return;
            } 
            this.pictureBox1.Visible = false;
            this.videoSourcePlayer1.Visible = true;
            //videoDevices[Indexof]确定出用哪个摄像头了。
            videoSource = new VideoCaptureDevice(videoDevices[Indexof].MonikerString);
            //设置下像素，这句话不写也可以正常运行：
            videoSource.VideoResolution = videoSource.VideoCapabilities[0];

            //videoSource.SetCameraProperty(CameraControlProperty.Exposure, -7, CameraControlFlags.Auto);
            //videoSource.DisplayCrossbarPropertyPage(IntPtr.Zero);
            //在videoSourcePlayer1里显示
            videoSourcePlayer1.VideoSource = videoSource;
            videoSourcePlayer1.Start();
        }

        //拍照//这里多了一个pictureBox1，想的是展示下抓拍的照片
        private void showImg_Click(object sender, EventArgs e)
        {
            if (videoSource == null)
            {
                MessageBox.Show("请先打开摄像头");
                return;
            }
            //videoSourcePlayer继承Control父类，定义 GetCurrentVideoFrame能输出bitmap
            bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
            pictureBox1.Image = bitmap;
            this.videoSourcePlayer1.Visible = false;
            this.pictureBox1.Visible = true;
            //这里停止摄像头继续工作 当然videoSourcePlayer里也定义了 Stop();用哪个都行
            videoSourcePlayer1.Stop();
            // videoSourcePlayer1.SignalToStop(); videoSourcePlayer1.WaitForStop();
        }

        private void rescratch_Click(object sender, EventArgs e)
        {
            Indexof = Cameralist.SelectedIndex;
            if (Indexof < 0)
            {
                MessageBox.Show("请选择一个摄像头");
                return;
            }
            this.pictureBox1.Visible = false;
            this.videoSourcePlayer1.Visible = true;
            //videoDevices[Indexof]确定出用哪个摄像头了。
            videoSource = new VideoCaptureDevice(videoDevices[Indexof].MonikerString);
            //设置下像素，这句话不写也可以正常运行：
            videoSource.VideoResolution = videoSource.VideoCapabilities[0];

            //videoSource.SetCameraProperty(CameraControlProperty.Exposure, -7, CameraControlFlags.Manual);
            //在videoSourcePlayer1里显示
            videoSourcePlayer1.VideoSource = videoSource;
            videoSourcePlayer1.Start();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (bitmap == null)
            {
                MessageBox.Show("没有图像！");
                return;
            }
            else
            {
                img_path = "source//" + user_num + "//";
                if (Directory.Exists(img_path) == false)
                {
                    Directory.CreateDirectory(img_path);
                }
                img_path = "source//" + user_num + "//" + user_num + "_1.jpg";
                bitmap.Save(img_path);
                
                //recognize.test_single(bitmap);
                isok = true;
                MessageBox.Show("保存成功！");
                this.Close();
                //return;
            }
        }

        private void Cameralist_SelectedIndexChanged(object sender, EventArgs e)
        {
            videoSourcePlayer1.Stop();
            Indexof = Cameralist.SelectedIndex;
        }

        private void addFace_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Indexof >= 0)
            {
                videoSourcePlayer1.Stop();
            }
        }
    }
}
