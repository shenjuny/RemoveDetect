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
using System.Data.SqlClient;

namespace RemoveDetect
{
    public partial class _1_menuList : Form
    {
        public Bitmap bmp_first;
        public List<string> tools_first = new List<string>();
        public Bitmap bmp_secend;
        public List<string> tools_second = new List<string>();
        BackgroundWorker worker = new BackgroundWorker();
        FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        public delegate void RecEventHandler(byte[] queueByte);
        private List<byte> buffer = new List<byte>(4096);
        public string userID;
        public string loginTime;
        public _1_menuList()
        {
            InitializeComponent();
        }
        public _1_menuList(string user, string login_time)
        {
            InitializeComponent();
            userID = user;
            loginTime = login_time;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            _1_menuLogout exit = new _1_menuLogout();
            exit.ShowDialog(this);
        }
        /// <summary>
        /// 物品管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread thread = new Thread(showform);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform()
        {
            Application.Run(new _2_toolManage());
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



        /// <summary>
        /// 工具取放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

            this.Close();
            Thread thread = new Thread(showform4);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform4()
        {
            Application.Run(new _2_toolPickBack());
        }
        /// <summary>
        /// 设备配置管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread thread = new Thread(showform5);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform5()
        {
            Application.Run(new _5_toolConfig());
        }
        /// <summary>
        /// 人员管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread thread = new Thread(showform3);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform3()
        {
            Application.Run(new _3_userManage());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread thread = new Thread(showform2);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform2()
        {
            Application.Run(new _4_check());
        }

        private void _1_menuList_Load(object sender, EventArgs e)
        {
            //创建数据库连接类的对象
            SqlConnection con = new SqlConnection("server=.;database=RD;user=sa;pwd=Sa123456");
            //将连接打开
            con.Open();
            //执行con对象的函数，返回一个SqlCommand类型的对象
            SqlCommand cmd = con.CreateCommand();
            //把输入的数据拼接成sql语句，并交给cmd对象
            cmd.CommandText = "select * from device";
            //用cmd的函数执行语句，返回SqlDataReader对象dr,dr就是返回的结果集（也就是数据库中查询到的表数据）
            SqlDataReader dr = cmd.ExecuteReader();
            //用dr的read函数，每执行一次，返回一个包含下一行数据的集合dr，在执行read函数之前，dr并不是集合

            if (dr.Read()&& dr["Num"].ToString() != "" && dr["layer"].ToString() != "" && dr["row"].ToString() != "" && dr["col"].ToString() != "" && dr["type"].ToString() != "" && dr["size"].ToString() != "")
            {
                //dr[]里面可以填列名或者索引，显示获得的数据</span>
                //MessageBox.Show(dr["Num"].ToString());
                //用完后关闭连接，以免影响其他程序访问
                con.Close();
            }
            else
            {
                //用完后关闭连接，以免影响其他程序访问
                con.Close();
                this.Close();
                Thread thread = new Thread(showform6);
                thread.IsBackground = false;
                thread.Start();
            }
        }
        void showform6()
        {
            Application.Run(new _6_InitConfig());
        }
    }
}
