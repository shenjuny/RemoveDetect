using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using System.Threading;
using System.Drawing.Imaging;
using AForge.Imaging;
using System.IO;
using System.IO.Ports;
using AForge.Imaging.Filters;

namespace RemoveDetect
{
    public partial class Main : Form
    {
        SerialPort sp1 = new SerialPort();
        string received;
        private VideoCaptureDevice videoSource;
        public Bitmap bmp_first;
        public List<string> tools_first= new List<string>();
        public Bitmap bmp_secend;
        public List<string> tools_second = new List<string>();
        BackgroundWorker worker = new BackgroundWorker();
        bool first_stop = false;
        bool first_close = false;
        FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        public delegate void RecEventHandler(byte[] queueByte);
        private List<byte> buffer = new List<byte>(4096);
        bool iscontrol = false;
        Thread thread;
        bool isFirst =true;
        public Main()
        {
            InitializeComponent();
             
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            worker.WorkerReportsProgress = true;
            worker.DoWork += dowork;
            worker.ProgressChanged += report;
        }
        public Main(string user,string login_time)
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            worker.WorkerReportsProgress = true;
            worker.DoWork += dowork;
            lbl_user.Text = user;
            lbl_time.Text = login_time;
        }
        private void report(object sender, ProgressChangedEventArgs e)
        {
            SerialPortLoad();
        }
        private void dowork(object sender, DoWorkEventArgs e)
        {
            worker.ReportProgress(0);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //logon form2 = new logon();
            //form2.ShowDialog();
            //if (form2.isCancel)
            //{
            //    this.Close();
            //    return;
            //}
            //lbl_user.Text = form2.user;
            //lbl_time.Text = form2.login_time;
            worker.RunWorkerAsync(0);
        }
        
        private void toolStripButtonConfig_Click(object sender, EventArgs e)
        {
            iscontrol = true;
            this.videoSourcePlayer1.Stop();
            config cf = new config();
            cf.setControlValue += new setControlValue(_setControlValue);
            cf.Show();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        public void _setControlValue(bool control)
        {
            iscontrol= control;
            thread = new Thread(new ParameterizedThreadStart(openCamera1));
            thread.IsBackground = true;
            thread.Start();
            isFirst = false;
        }
        Bitmap ConvertToFormat(System.Drawing.Image image, PixelFormat format)
        {
            Bitmap copy = new Bitmap(image.Width, image.Height, format);
            using (Graphics gr = Graphics.FromImage(copy))
                gr.DrawImage(image, new Rectangle(0, 0, copy.Width, copy.Height));
            return copy;
        }
        #region 串口传输

        /// <summary>
        /// 初始化
        /// </summary>
        public void SerialPortLoad()
        {
            string[] strCom = SerialPort.GetPortNames();
            if (strCom == null)
            {
                MessageBox.Show("本机没有串口！", "Error");
                return;
            }

            sp1.BaudRate = 19200;
            Control.CheckForIllegalCrossThreadCalls = false;
            sp1.DataReceived += new SerialDataReceivedEventHandler(Sp1_DataReceived);

            sp1.DtrEnable = true;//获取或设置一个值，该值在串行通信过程中启用数据终端就绪 (DTR) 信号。
            sp1.RtsEnable = true;//获取或设置一个值，该值指示在串行通信中是否启用请求发送 (RTS) 信号
            sp1.ReceivedBytesThreshold = 1;                     //设置数据读取超时为1秒
            sp1.Close();
            Open_Com();
            //send(openDevice());//解锁
            txtbox_msg.Text = "待机中";
        }
        /// <summary>
        /// 接收串口信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sp1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (iscontrol)
            {
                return;
            }
            if (sp1.IsOpen)     //判断是否打开串口
            {
                //输出当前时间
                DateTime dt = DateTime.Now;
                try
                {
                    byte[] readBuffer = null;
                    int n = sp1.BytesToRead;
                    byte[] buf = new byte[n];
                    sp1.Read(buf, 0, n);

                    //1.缓存数据
                    buffer.AddRange(buf);
                    //2.完整性判断
                    while (buffer.Count >= 9)

                    //至少包含帧头（2字节）、长度（1字节）、校验位（1字节）；根据设计不同而不同
                    {
                        //2.1 查找数据头
                        if (buffer[0] == 0xDB) //传输数据有帧头，用于判断
                        {
                            int len = buffer[1];
                            if (buffer.Count < len) //数据区尚未接收完整
                            {
                                break;
                            }
                            readBuffer = new byte[len + 2];
                            buffer.CopyTo(0,readBuffer,0,len);
                       
                            buffer.RemoveRange(0, len);
                            //执行其他代码，对数据进行处理。
                            string x = "";
                            for (int i = 0; i < len+ 2; i++)
                            {
                                if (readBuffer[i] < 10)
                                {
                                    x += "0";
                                    x += Convert.ToString(readBuffer[i], 16);
                                }
                                else
                                {
                                    x += Convert.ToString(readBuffer[i], 16);
                                }
                            }
                            delData(x);
                        }
                        else //帧头不正确时，记得清除
                        {
                            buffer.RemoveAt(0);
                        }
                        
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "出错提示!!!!!");
                    received = "";
                }
            }
            else
            {
                MessageBox.Show("请打开某个串口", "错误提示");
            }
        }

        //处理接受到的内容
        private void delData(string content)
        {
            StreamWriter sW = new StreamWriter("log.txt", true);
            received = content;
            sW.WriteLine(received);
            sW.Close();
            sW.Dispose();
            //0x01：打开过程
            //0x02：关闭过程
            //0x03：停止
            //0x04：完全关闭
            //0x00~0x64：标识抽屉从0%~100%开启状态
            if (received.Length<14)
            {
                txtbox_msg.Text = "非法指令";
                return;
            }
            if (received.Substring(10, 2) == "03") //读取抽屉到位信号
            {
                txtbox_msg.Text = "抽屉停止";
            }
            if (received.Substring(10, 2) == "02") //读取抽屉到位信号
            {
                txtbox_msg.Text = "抽屉关闭";
            }
            if (received.Substring(10, 2) == "03" && first_stop == false) //读取抽屉到位信号
            {
                first_stop = true;
                FZ();
            }
            if (received.Substring(10, 2) == "02" && first_close == false&&first_stop) //读取抽屉正在关闭信号 比对正常
            {
                first_close = true;
                FZ2();
            }
            if (received.Substring(10, 2) == "04") //读取抽屉完全关闭信号
            {
                first_stop = false;
                first_close = false;

                tools_first.Clear();
                tools_second.Clear();
                txtbox_msg.Text = "待机中";
            }
            if (received.Substring(10, 2) == "01")
            {
                txtbox_msg.Text = "抽屉开启";
                if (isFirst)
                {
                    thread = new Thread(new ParameterizedThreadStart(openCamera1));
                    thread.IsBackground = true;
                    thread.Start();
                    isFirst = false;
                }
            }
        }
        private void openCamera()
        {
            if (this.videoSourcePlayer1.InvokeRequired)
            {
                shoot_first fc = new shoot_first(openCamera);
                this.Invoke(fc); //通过代理调用刷新方法
            }
            else
            {
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.VideoResolution = videoSource.VideoCapabilities[0];
                //videoSource.SetCameraProperty(CameraControlProperty.Exposure, -7, CameraControlFlags.Manual);
                videoSourcePlayer1.VideoSource = videoSource;
                videoSourcePlayer1.Start();
                //Thread.Sleep(2000);
            }
        }
        private void openCamera1(object obj)
        {
            openCamera();
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        private void Open_Com()
        {
            if (!sp1.IsOpen)
            {
                try
                {
                    //设置串口号
                    string serialName = "COM1";
                    sp1.PortName = serialName;

                    //设置各“串口设置”
                    string strBaudRate = "19200";
                    string strDateBits = "8";
                    Int32 iBaudRate = Convert.ToInt32(strBaudRate);
                    Int32 iDateBits = Convert.ToInt32(strDateBits);
                    sp1.BaudRate = iBaudRate;       //波特率
                    sp1.DataBits = iDateBits;       //数据位
                    sp1.StopBits = StopBits.One;//停止位
                    sp1.Parity = Parity.None;
                    if (sp1.IsOpen == true)//如果打开状态，则先关闭一下
                    {
                        sp1.Close();
                    }
                    sp1.Open();     //打开串口
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    return;
                }
            }
            else
            {
                sp1.Close();                    //关闭串口
            }
        }
        #endregion

        #region 检测流程
        private delegate void shoot_first(); //代理委托

        public static System.Drawing.Image AcquireRectangleImage1(System.Drawing.Image source, Rectangle rect)
        {
            if (source == null || rect.IsEmpty) return null;
            Bitmap bmSmall = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (Graphics grSmall = Graphics.FromImage(bmSmall))
            {

                grSmall.DrawImage(source, new System.Drawing.Rectangle(0, 0, bmSmall.Width, bmSmall.Height), rect, GraphicsUnit.Pixel);
                grSmall.Dispose();
            }
            return bmSmall;
        }
        private void FZ()
        {
            Thread.Sleep(500);
            tools_first.Clear();
            bmp_first = videoSourcePlayer1.GetCurrentVideoFrame();
            //pictureBox1.Image = bmp_first;
            //bmp_first.Save("result\\2.jpg");
            this.videoSourcePlayer1.Visible = false;
            this.pictureBox1.Visible = true;
            DirectoryInfo root = new DirectoryInfo("tools//");
            DirectoryInfo[] dics = root.GetDirectories();
            Bitmap x = ConvertToFormat(bmp_first, PixelFormat.Format24bppRgb);
            progressBar.Value = 0;
            progressBar.Maximum = dics.Length;
            try
            {
                txtbox_msg.Text = "图像处理中";
                foreach (var path in dics)
                {
                    Bitmap sourceImage = ConvertToFormat(System.Drawing.Image.FromFile(path.GetFiles()[0].FullName), PixelFormat.Format24bppRgb);
                    // 创建模板匹配算法的实例
                    //（将相似度阈值设置为92.5％）
                    string temp = path.Name;
                    DataTable dataTable = DBSet.ReadCSV(@"..\tool.csv");
                    List<tool> listTool = Common.ToDataList<tool>(dataTable);
                    foreach (tool a in listTool)
                    {
                        if (a.picture == temp)
                        {
                            Rectangle p = new Rectangle(Convert.ToInt32(a.x) - 10, Convert.ToInt32(a.y) - 10, Convert.ToInt32(a.w)  +20, Convert.ToInt32(a.h) + 20);
                            Bitmap targetImage = (Bitmap)AcquireRectangleImage1(x,p);
                            Bitmap cp = targetImage.Clone() as Bitmap; // 复制原始图像

                            FiltersSequence seq = new FiltersSequence();
                            seq.Add(Grayscale.CommonAlgorithms.BT709);  // 添加灰度滤镜
                            seq.Add(new OtsuThreshold()); // 添加二值化滤镜
                            cp = seq.Apply(targetImage); // 应用滤镜
                            cp = ConvertToFormat(cp, PixelFormat.Format24bppRgb);
                            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.85f);
                            //查找具有上述指定相似性的所有匹配项
                            TemplateMatch[] matchings = tm.ProcessImage(cp, sourceImage);
                            if (matchings.Length != 0)
                            {
                                tools_first.Add(path.Name);
                                //高亮发现的匹配
                                using (Graphics g = Graphics.FromImage(targetImage))
                                {
                                    if (matchings.Length != 0)
                                    {
                                        foreach (TemplateMatch m in matchings)
                                        {
                                            g.DrawRectangle(new Pen(Color.Red, 1), m.Rectangle);
                                        }
                                    }
                                }
                                
                                //using (Graphics g = Graphics.FromImage(x))
                                //{
                                //            g.DrawRectangle(new Pen(Color.Red, 1), p);
                                //}
                                Random rd = new Random();
                                targetImage.Save("result\\" + path.Name+ rd.Next(0, 100) + "1.jpg");
                                break;
                            }
                            
                        }
                        
                    }
                    sourceImage.Dispose();
                    progressBar.Value++;
                }
                
                txtbox_msg.Text = "处理完成!!!";
                this.pictureBox1.Visible = false;
                pictureBox1.Image = x;
                progressBar.Value = 0;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void FZ2()
        {

            tools_second.Clear();
            bmp_secend = videoSourcePlayer1.GetCurrentVideoFrame();
            pictureBox1.Image = bmp_secend;
            this.videoSourcePlayer1.Visible = true;
            this.pictureBox1.Visible = false;
            DirectoryInfo root = new DirectoryInfo("tools//");
            DirectoryInfo[] dics = root.GetDirectories();
            progressBar.Value = 0;
            progressBar.Maximum = dics.Length;
            Bitmap x = ConvertToFormat(bmp_secend, PixelFormat.Format24bppRgb);
            try
            {
                txtbox_msg.Text = "图像处理中";
                foreach (var path in dics)
                {
                    Bitmap sourceImage = ConvertToFormat(System.Drawing.Image.FromFile(path.GetFiles()[0].FullName), PixelFormat.Format24bppRgb);
                    // 创建模板匹配算法的实例
                    //（将相似度阈值设置为92.5％）
                    string temp = path.Name;
                    DataTable dataTable1 = DBSet.ReadCSV(@"..\tool.csv");
                    List<tool> listTool1 = Common.ToDataList<tool>(dataTable1);
                    foreach (tool a in listTool1)
                    {
                        if (a.picture == temp)
                        {
                            Rectangle p = new Rectangle(Convert.ToInt32(a.x) - 10, Convert.ToInt32(a.y) - 10, Convert.ToInt32(a.w) + 20, Convert.ToInt32(a.h) + 20);
                            Bitmap targetImage = (Bitmap)AcquireRectangleImage1(x, p);
                            Bitmap cp = targetImage.Clone() as Bitmap; // 复制原始图像

                            FiltersSequence seq = new FiltersSequence();
                            seq.Add(Grayscale.CommonAlgorithms.BT709);  // 添加灰度滤镜
                            seq.Add(new OtsuThreshold()); // 添加二值化滤镜
                            cp = seq.Apply(targetImage); // 应用滤镜
                            cp = ConvertToFormat(cp, PixelFormat.Format24bppRgb);
                            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.85f);
                            //查找具有上述指定相似性的所有匹配项
                            TemplateMatch[] matchings = tm.ProcessImage(cp, sourceImage);
                            if (matchings.Length != 0)
                            {
                                tools_second.Add(path.Name);
                                //高亮发现的匹配
                                using (Graphics g = Graphics.FromImage(targetImage))
                                {
                                    if (matchings.Length != 0)
                                    {
                                        foreach (TemplateMatch m in matchings)
                                        {
                                            g.DrawRectangle(new Pen(Color.Red, 1), m.Rectangle);
                                        }
                                    }
                                }
                                //using (Graphics g = Graphics.FromImage(x))
                                //{
                                //    g.DrawRectangle(new Pen(Color.Red, 1), p);
                                //}
                                //Random rd = new Random();
                                //targetImage.Save("result\\" + path.Name + rd.Next(0, 100) + "2.jpg");
                                //break;
                            }
                        }
                    }
                    sourceImage.Dispose();
                    progressBar.Value++;
                    dataTable1.Dispose();
                }
                List<string> pick = new List<string>();
                List<string> putIn = new List<string>();
               


                if (tools_first.Count >= tools_second.Count)
                {
                    pick = (tools_first.Distinct()).Except(tools_second.Distinct()).ToList();
                   
                }
                if(tools_first.Count <= tools_second.Count)
                {
                    putIn = (tools_second.Distinct()).Except(tools_first.Distinct()).ToList();
                }
                progressBar.Value = 0;
                DataTable dataTable = DBSet.ReadCSV(@"..\tool.csv");
                List<tool> listTool = Common.ToDataList<tool>(dataTable);
                if (pick.Count != 0|| putIn.Count !=0)
                {
                    dataGridView1.Rows.Clear();
                    
                    foreach (var i in pick)
                    {
                        foreach (tool a in listTool)
                        {
                            if (a.picture == i)
                            {
                                int index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = "领取";
                                dataGridView1.Rows[index].Cells[1].Value = a.No;
                                dataGridView1.Rows[index].Cells[2].Value = a.name;
                                dataGridView1.Rows[index].Cells[3].Value = a.address;
                                dataGridView1.Rows[index].Cells[4].Value = ConvertToFormat(System.Drawing.Image.FromFile("tools//" + i + "//" + i + "_2.jpg"), PixelFormat.Format24bppRgb);
                                dataGridView1.Rows[index].Cells[5].Value = DateTime.Now.ToString("HH:mm:ss");
                            }
                        }
                    }
                    foreach (var j in putIn)
                    {
                       
                        foreach (tool a in listTool)
                        {
                            if (a.picture == j)
                            {
                                int index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = "归还";
                                dataGridView1.Rows[index].Cells[1].Value = a.No;
                                dataGridView1.Rows[index].Cells[2].Value = a.name;
                                dataGridView1.Rows[index].Cells[3].Value = a.address;
                                dataGridView1.Rows[index].Cells[4].Value = ConvertToFormat(System.Drawing.Image.FromFile("tools//" + j + "//" + j + "_2.jpg"), PixelFormat.Format24bppRgb);
                                dataGridView1.Rows[index].Cells[5].Value = DateTime.Now.ToString("HH:mm:ss");
                            }
                        }
                    }
                }
                dataTable.Dispose();
                txtbox_msg.Text = "处理完成!!!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.videoSourcePlayer1.IsRunning)
            {
                this.videoSourcePlayer1.Stop();
                thread.Abort();
            }
        }
    }
}
