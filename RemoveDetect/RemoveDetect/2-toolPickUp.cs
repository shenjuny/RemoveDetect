using AForge.Video.DirectShow;
using RDService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RemoveDetect
{
    public partial class _2_toolPickUp : Form
    {

        private List<byte> buffer = new List<byte>(4096);
        string received;
        bool first_stop = false;
        bool first_close = false;
        public darknet detector;
        private VideoCaptureDevice videoSource;
        FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        AForge.Controls.VideoSourcePlayer vp = new AForge.Controls.VideoSourcePlayer();
        Service1 x = new Service1();
        public Bitmap bmp_first;
        public List<box> tools_first = new List<box>();
        public Bitmap bmp_secend;
        public List<box> tools_second = new List<box>();
        public _2_toolPickUp()
        {
            InitializeComponent();
            SerialPortLoad();
        }

        private void home_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread thread = new Thread(showform1);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform1()
        {
            Application.Run(new _1_menuList());
        }

        private void exit_Click(object sender, EventArgs e)
        {
            _1_menuLogout exit = new _1_menuLogout();
            exit.ShowDialog(this);
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
            //txtbox_msg.Text = "待机中";
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
                    sp1.BaudRate = 19200;       //波特率
                    sp1.DataBits = 8;       //数据位
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


        /// <summary>
        /// 接收串口信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sp1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
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
                            buffer.CopyTo(0, readBuffer, 0, len);
                            buffer.RemoveRange(0, len);
                            //执行其他代码，对数据进行处理。
                            string x = "";
                            for (int i = 0; i < len + 2; i++)
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
            received = content;
            //0x01：打开过程
            //0x02：关闭过程
            //0x03：停止
            //0x04：完全关闭
            //0x00~0x64：标识抽屉从0%~100%开启状态
            if (received.Length < 14)
            {
                return;
            }
            if (received.Substring(10, 2) == "03" && first_stop == false) //读取抽屉到位信号
            {
                lblOpen.Text = "所有抽屉已打开，请选择一个抽屉拿取工具";
                first_stop = true;
                FZ();
            }
            if (received.Substring(10, 2) == "02" && first_close == false && first_stop) //读取抽屉正在关闭信号 比对正常
            {
                lblOpen.Text = "用扫码枪扫描确认所取工具";
                first_close = true;
                //FZ2();
            }
            if (received.Substring(10, 2) == "04") //读取抽屉完全关闭信号
            {
                first_stop = false;
                first_close = false;
                tools_first.Clear();
                tools_second.Clear();
            }
        }
        Bitmap ConvertToFormat(System.Drawing.Image image, PixelFormat format)
        {
            Bitmap copy = new Bitmap(image.Width, image.Height, format);
            using (Graphics gr = Graphics.FromImage(copy))
                gr.DrawImage(image, new Rectangle(0, 0, copy.Width, copy.Height));
            return copy;
        }
        private void FZ()
        {
            Thread.Sleep(500);
            tools_first.Clear();
            bmp_first = vp.GetCurrentVideoFrame();
            Bitmap x = ConvertToFormat(bmp_first, PixelFormat.Format24bppRgb);
            try
            {
                MemoryStream ms = new MemoryStream();
                x.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bytes = ms.GetBuffer();  //byte[]   bytes=   ms.ToArray(); 这两句都可以，至于区别么，下面有解释
                ms.Close();
                darknet.bbox_t[] z = darknet.Detect(bytes);
                using (Graphics g = Graphics.FromImage(x))
                {
                    for (int i = 0; i < z.Length; i++)
                    {

                        //if (z[i].h > 0 && z[i].prob > 0.8)
                        //每个检测到的实例需要进行下一阶段测量
                        if (z[i].h > 0 && z[i].prob > 0.5)
                        {
                            box bb = new box();
                            //"目标检测结果：" + darknet.mark[Convert.ToInt32(z[i].obj_id)]
                            //"概率：" + z[i].prob
                            bb.obj_id = Convert.ToString(z[i].obj_id);
                            bb.prob = z[i].prob;
                            //bb.x = 
                            //Rectangle p = new Rectangle(Convert.ToInt32(z[i].x), Convert.ToInt32(z[i].y), Convert.ToInt32(z[i].w), Convert.ToInt32(z[i].h));
                            //toolStripStatusLabel2.Text = darknet.mark[Convert.ToInt32(z[i].obj_id)];
                            //int index = dataGridView1.Rows.Add();
                            //dataGridView1.Rows[index].Cells[0].Value = darknet.mark[Convert.ToInt32(z[i].obj_id)];
                            //dataGridView1.Rows[index].Cells[1].Value = targetImage;
                            //dataGridView1.Rows[index].Cells[2].Value = DateTime.Now.ToString("HH:mm:ss");
                            //x = ca.drawBmp(x, Convert.ToInt32(z[i].x), Convert.ToInt32(z[i].y), Convert.ToInt32(z[i].w), Convert.ToInt32(z[i].h), Color.Red, 5);
                        }
                    }
                }
                pictureBox1.Image = x;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        private void _2_toolPickUp_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(loading);
            thread.IsBackground = false;
            thread.Start();
            string deviceID = "1";
            //获取时间配置
            var tmp = x.searchTime(deviceID);
            lblTime.Text = "请在" + (tmp + ":00") + "内完成工具取用";

            //提示信息（标题、用户打开抽屉）
            this.lblChoice.Font = new Font("宋体", 9, FontStyle.Bold);
            lblOpen.Text = "请打开抽屉";
        }
        void loading()
        {
            //加载摄像头
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videoDevices[1].MonikerString);
            videoSource.VideoResolution = videoSource.VideoCapabilities[0];
            vp.VideoSource = videoSource;
            if (videoSource == null)
            {
                MessageBox.Show("请先打开摄像头");
                return;
            }
            vp.Start();
            //加载检测模型
            detector = new darknet("yolov3-voc.cfg", "yolov3-voc_last.weights", 0);
            detector.detectorInit();
        }

        void scan(string code)
        {
            if (true)
            {
                //创建数据库连接类的对象
                SqlConnection con = new SqlConnection("server=.;database=RD;user=sa;pwd=Sa123456");
                //将连接打开
                con.Open();
                //执行con对象的函数，返回一个SqlCommand类型的对象
                SqlCommand cmd = con.CreateCommand();
                //把输入的数据拼接成sql语句，并交给cmd对象
                cmd.CommandText = "select * from [tools] where Number ='" + code + "'";
                //用cmd的函数执行语句，返回SqlDataReader对象dr,dr就是返回的结果集（也就是数据库中查询到的表数据）
                SqlDataReader dr = cmd.ExecuteReader();
                //用dr的read函数，每执行一次，返回一个包含下一行数据的集合dr，在执行read函数之前，dr并不是集合

                if (dr.Read())
                {

                }
            }
        }
    }
}
