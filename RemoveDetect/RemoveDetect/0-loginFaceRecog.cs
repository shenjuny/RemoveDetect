using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Threading;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using RDService;
using System.Data.SqlClient;

namespace RemoveDetect
{
    public delegate void islegalUser(string textValue);
    public partial class faceRecog : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        Bitmap bitmap;
        model.faceDetectedObj result;
        int count = 0;
        string login_time;
        string user;
        //Service1 x = new Service1();
        public faceRecog()
        {
            InitializeComponent();
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            this.pictureBox1.Visible = true;
            this.videoSourcePlayer1.Visible = false;
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.VideoResolution = videoSource.VideoCapabilities[0];
            videoSourcePlayer1.VideoSource = videoSource;
            if (videoSource == null)
            {
                MessageBox.Show("请先打开摄像头");
                return;
            }
            videoSourcePlayer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (bitmap != null&&count<=20)
            {
                try
                {
                    result = null;
                    model t = new model();
                    t.SetTrainedFaceRecognizer(model.FaceRecognizerType.LBPHFaceRecognizer);
                    Image<Bgr, byte> currentFrame = new Image<Bgr, byte>(bitmap);      
                    Mat invert = new Mat();
                    CvInvoke.BitwiseAnd(currentFrame, currentFrame, invert);
                    result = t.faceRecognize(invert);
                    if (result.names.Count > 0)
                    {
                        
                        //var tmp = x.logon(null, null, null, result.names[0]);
                        //创建数据库连接类的对象
                        SqlConnection con = new SqlConnection("server=.;database=RD;user=sa;pwd=Sa123456");
                        //将连接打开
                        con.Open();
                        //执行con对象的函数，返回一个SqlCommand类型的对象
                        SqlCommand cmd = con.CreateCommand();
                        //把输入的数据拼接成sql语句，并交给cmd对象
                        cmd.CommandText = "select * from [user] where face ='" + result.names[0]+"'";
                        //用cmd的函数执行语句，返回SqlDataReader对象dr,dr就是返回的结果集（也就是数据库中查询到的表数据）
                        SqlDataReader dr = cmd.ExecuteReader();
                        //用dr的read函数，每执行一次，返回一个包含下一行数据的集合dr，在执行read函数之前，dr并不是集合

                        if (dr.Read())
                        {
                            //dr[]里面可以填列名或者索引，显示获得的数据</span>
                            //MessageBox.Show(dr["Num"].ToString());
                            login_time = DateTime.Now.ToString("");
                            user = dr["id"].ToString();
                            timer1.Enabled = false;
                            timer2.Enabled = false;
                            this.videoSourcePlayer1.Stop();
                            this.videoSourcePlayer1.Dispose();
                            this.Close();
                            Thread thread = new Thread(showform);
                            thread.IsBackground = false;
                            thread.Start();
                            //用完后关闭连接，以免影响其他程序访问
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show("用户名或密码错误！请重新输入！");
                        }
                    }
                    count++;
                }
                catch (Exception ex)
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    MessageBox.Show("错误提示："+ ex.Message);
                }
            }
            else
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                this.videoSourcePlayer1.Stop();
                this.videoSourcePlayer1.Dispose();
                this.videoSourcePlayer1.Visible = false;
                this.pictureBox1.Visible = true;
                errorLogin error = new errorLogin();
                error.ShowDialog(this);
            }
        }
        
        void showform()
        {
            Application.Run(new _1_menuList(user,login_time));
        }

        private void btn_face_recog_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Visible = false;
            this.videoSourcePlayer1.Visible = true;
            bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
            pictureBox1.Image = bitmap;
            count = 0;
            timer2.Enabled = true;
            timer1.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            this.videoSourcePlayer1.Stop();
            this.videoSourcePlayer1.Dispose();
            loginChoice lc = new loginChoice();
            lc.ShowDialog(this);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
        }
    }
}
