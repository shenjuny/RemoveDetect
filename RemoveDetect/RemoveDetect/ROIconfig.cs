using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using AForge.Imaging.Filters;

namespace RemoveDetect
{
    public delegate void setToolTextValue(string textValue);
    public partial class ROIconfig : Form
    {
        public string labelmsg = "";
        public string labelmsg_num = "";
        public string select_pic = "";
        public static Bitmap bmp;
        public static Bitmap roibmp;
        public static Bitmap tools;
        bool isRoi = false;
        Point mouseDownPoint;
        Point mouseUpPoint;
        List<int> roi = new List<int>();

        Dictionary<Point, Point> dicPoints = new Dictionary<Point, Point>();
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        Bitmap bitmap;
        public event setToolTextValue setToolTextValue;
        public bool isok = false;
        public string img_path = "";
        public string img_path2 = "";
        public string tool_num = "";
        public ROIconfig(string tool_ID)
        {
            InitializeComponent();
            tool_num = tool_ID;
        }
        void Unanble()
        {
            btn_camera_open.Enabled = false;
            btn_scratch.Enabled = false;
            //btn_save.Enabled = false;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            this.pbox.Visible = false;
            this.videoSourcePlayer1.Visible = true;
            //videoDevices[Indexof]确定出用哪个摄像头了。
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            //设置下像素，这句话不写也可以正常运行：
            videoSource.VideoResolution = videoSource.VideoCapabilities[0];
           
            //videoSource.SetCameraProperty(CameraControlProperty.Exposure, -7, CameraControlFlags.Manual);

            //在videoSourcePlayer1里显示
            videoSourcePlayer1.VideoSource = videoSource;
            videoSourcePlayer1.Start();
            if (videoSource == null)
            {
                MessageBox.Show("请先打开摄像头");
                return;
            }
            //videoSourcePlayer继承Control父类，定义 GetCurrentVideoFrame能输出bitmap
            bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
            pbox.Image = bitmap;
        }
        //拍一张
        private void btn_scratch_Click(object sender, EventArgs e)
        {
            bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
            pbox.Image = bitmap;
            TimeSpan ts = DateTime.Now - DateTime.Parse("1970-1-1");
            pbox.Image.Save(Convert.ToInt32(ts.TotalSeconds).ToString() + ".jpg");
            //this.videoSourcePlayer1.Visible = false;
            //this.pbox.Visible = true;
            //videoSourcePlayer1.Stop();
            //try
            //{
            //    //model t = new model();
            //    //t.SetTrainedFaceRecognizer(model.FaceRecognizerType.LBPHFaceRecognizer);
            //    Image<Bgr, byte> currentFrame = new Image<Bgr, byte>(bitmap);  //只能这么转           
            //    Mat invert = new Mat();
            //    CvInvoke.BitwiseAnd(currentFrame, currentFrame, invert);  //这是官网上的方法，变通用。没看到提供其它方法直接转换的。
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //isRoi = false;
            //roi.Clear();
            //dicPoints.Clear();
        }
        //连续拍
        private void camera_open_Click(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            this.pbox.Visible = false;
            this.videoSourcePlayer1.Visible = true;
            //videoDevices[Indexof]确定出用哪个摄像头了。
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            //设置下像素，这句话不写也可以正常运行：
            videoSource.VideoResolution = videoSource.VideoCapabilities[0];
            //在videoSourcePlayer1里显示
            videoSourcePlayer1.VideoSource = videoSource;
            videoSourcePlayer1.Start();
            bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
            pbox.Image = bitmap; 
        }

        private void pbox_MouseDown(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(label);
            mouseDownPoint = new Point(e.X, e.Y); isRoi = false; getROI(e.X, e.Y);
        }

        private void pbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (pbox.Image != null) drawCross2(e.X, e.Y);
        }

        private void pbox_MouseUp(object sender, MouseEventArgs e)
        {
            Pen p = new Pen(Color.Blue, 2);
            isRoi = true;
            pbox.Refresh();
            Graphics g = pbox.CreateGraphics();

            g.DrawRectangle(p, mouseDownPoint.X, mouseDownPoint.Y, e.X - mouseDownPoint.X, e.Y - mouseDownPoint.Y);

            g.Dispose();

            mouseUpPoint = new Point(e.X, e.Y);

            getROI(e.X, e.Y);

            //实时的画之前已经画好的矩形
            Graphics f = pbox.CreateGraphics();
            foreach (var item in dicPoints)
            {

                Point p1 = item.Key;
                Point p2 = item.Value;
                f.DrawRectangle(p, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);

            }
            p.Dispose();
        }

        void drawCross2(int x, int y)
        {
            if (!isRoi)
            {
                pbox.Refresh();
                Graphics g = pbox.CreateGraphics();
                Pen p = new Pen(Color.Red, 1);

                int w = pbox.Width; int h = pbox.Height;

                g.DrawLine(p, new Point(0, y), new Point(w, y));
                g.DrawLine(p, new Point(x, 0), new Point(x, h));
                p.Dispose(); g.Dispose();

            }

        }

        void getROI(int x, int y)
        {
            if (pbox.Image == null) return;

            int originalWidth = pbox.Image.Width;//原始尺寸
            int originalHeight = pbox.Image.Height;

            System.Reflection.PropertyInfo rectangleProperty = pbox.GetType().GetProperty("ImageRectangle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            Rectangle rectangle = (Rectangle)rectangleProperty.GetValue(pbox, null);

            int currentWidth = rectangle.Width;//缩放状态图片尺寸
            int currentHeight = rectangle.Height;

            double rate = (double)currentHeight / (double)originalHeight;//缩放比率

            int black_left_width = (currentWidth == pbox.Width) ? 0 : (pbox.Width - currentWidth) / 2;//左留白宽度
            int black_top_height = (currentHeight == pbox.Height) ? 0 : (pbox.Height - currentHeight) / 2;//上留白高度

            int zoom_x = x - black_left_width;//缩放图中鼠标坐标
            int zoom_y = y - black_top_height;

            double original_x = (double)zoom_x / rate;//原始图中鼠标坐标
            double original_y = (double)zoom_y / rate;
            roi.Add(Convert.ToInt32(original_x)); roi.Add(Convert.ToInt32(original_y));
            if (isRoi)
            {
                dicPoints.Add(mouseDownPoint, mouseUpPoint);
                tools = (Bitmap)AcquireRectangleImage(pbox.Image, new System.Drawing.Rectangle(roi[0], roi[1], roi[2] - roi[0], roi[3] - roi[1]));
               
                Bitmap cp = tools.Clone() as Bitmap; // 复制原始图像

                FiltersSequence seq = new FiltersSequence();
                seq.Add(Grayscale.CommonAlgorithms.BT709);  // 添加灰度滤镜
                seq.Add(new OtsuThreshold()); // 添加二值化滤镜
                cp = seq.Apply(tools); // 应用滤镜
                pictureBox1.Image = cp;
                //roi.Clear();
            }
        }
        /// <summary>
        /// 截取图像的矩形区域
        /// </summary>
        /// <param name="source">源图像对应picturebox1</param>
        /// <param name="rect">矩形区域，如上初始化的rect</param>
        /// <returns>矩形区域的图像</returns>
        public static Image AcquireRectangleImage(Image source, Rectangle rect)
        {
            if (source == null || rect.IsEmpty) return null;
            Bitmap bmSmall = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            //Bitmap bmSmall = new Bitmap(rect.Width, rect.Height, source.PixelFormat);
            using (Graphics grSmall = Graphics.FromImage(bmSmall))
            {

                grSmall.DrawImage(source, new System.Drawing.Rectangle(0, 0, bmSmall.Width, bmSmall.Height), rect, GraphicsUnit.Pixel);
                grSmall.Dispose();
            }
            return bmSmall;
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            //if (pictureBox1.Image == null)
            //{
            //    MessageBox.Show("没有图像！");
            //    return;
            //}
            //else
            //{
            //    img_path = "tools//" + tool_num + "//";
            //    img_path2 = "tools//" + tool_num + "//";
            //    if (Directory.Exists(img_path) == false)
            //    {
            //        Directory.CreateDirectory(img_path);
            //    }
            //    img_path += tool_num + "_1.jpg";
            //    img_path2 += tool_num + "_2.jpg";
            //    pictureBox1.Image.Save(img_path);
            //    tools.Save(img_path2);
            //    isok = true;
            //    MessageBox.Show("保存成功！");
            //}
            TimeSpan ts = DateTime.Now - DateTime.Parse("1970-1-1");
            pbox.Image.Save(Convert.ToInt32(ts.TotalSeconds).ToString()+ ".jpg");
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            isRoi = false;
            roi.Clear();
            dicPoints.Clear();
            pbox.Image = bmp;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isok)
            {
                //3、准备要回传的数据。  
                setToolTextValue(roi[0].ToString()+","+roi[1] + "," + (roi[2] - roi[0]) + "," + (roi[3] - roi[1]));
                this.Close();
                return;
            }
        }

        //关闭窗体，释放相机
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (btn_camera_open.Enabled == true)
            {
                videoSourcePlayer1.Stop();
            }

        }
        private void btn_openfile_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.Stop();
            string SelectedPath = "tools\\";
            DirectoryInfo root = new DirectoryInfo(SelectedPath);
            DirectoryInfo[] dics = root.GetDirectories();
            foreach (var path in dics)
            {
                FileInfo[] files = path.GetFiles("*.jpg");
                if (files.Length != 0)
                {
                    foreach (FileInfo file in files)
                    {
                        image_listBox.Items.Insert(0, file.Name);
                    }
                }
            }
        }

        private void point_listBox_DoubleClick(object sender, EventArgs e)
        {
        }
    }
}
