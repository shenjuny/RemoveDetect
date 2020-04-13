using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using RDService;
using System.Data.SqlClient;

namespace RemoveDetect
{
    public partial class idCardLogin : Form
    {
        string login_time;
        string user;

        //Service1 x = new Service1();
        int sum;
        public idCardLogin()
        {
            InitializeComponent();
            timer1.Enabled = true;
            sum = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            loginChoice lc = new loginChoice();
            lc.ShowDialog(this);
        }
        [DllImport("umf.DLL", EntryPoint = "fw_init")]
        public static extern Int32 fw_init(Int16 port, Int32 baud);
        [DllImport("umf.DLL", EntryPoint = "fw_exit")]
        public static extern Int32 fw_exit(Int32 icdev);

        [DllImport("umf.DLL", EntryPoint = "fw_card")]
        public static extern Int32 fw_card(Int32 icdev, Byte _Mode, ulong[] _Snr);

        Int32 ihdev;

        private void timer1_Tick(object sender, EventArgs e)
        {
            sum++;
            if (sum < 200)
            {
                ihdev = fw_init(100, 0);//baud 115200

                if (ihdev > 0)
                {
                    ulong[] cardnumber = new ulong[3];
                    int state = fw_card(ihdev, 0, cardnumber);
                    if (state != 0)
                    {
                        MessageBox.Show("卡片读取异常！");
                        fw_exit(ihdev);
                    }
                    else
                    {
                        //var tmp = x.logon(null, null, cardnumber[0].ToString(), null);
                        //创建数据库连接类的对象
                        SqlConnection con = new SqlConnection("server=.;database=RD;user=sa;pwd=Sa123456");
                        //将连接打开
                        con.Open();
                        //执行con对象的函数，返回一个SqlCommand类型的对象
                        SqlCommand cmd = con.CreateCommand();
                        //把输入的数据拼接成sql语句，并交给cmd对象
                        cmd.CommandText = "select * from [user] where IC ='" + cardnumber[0] + "'";
                        //用cmd的函数执行语句，返回SqlDataReader对象dr,dr就是返回的结果集（也就是数据库中查询到的表数据）
                        SqlDataReader dr = cmd.ExecuteReader();
                        //用dr的read函数，每执行一次，返回一个包含下一行数据的集合dr，在执行read函数之前，dr并不是集合

                        if (dr.Read())
                        {
                            login_time = DateTime.Now.ToString("");
                            user = dr["id"].ToString();
                            fw_exit(ihdev);
                            timer1.Enabled = false;
                            this.Close();
                            _0_loginOK ok = new _0_loginOK();
                            ok.ShowDialog(this);
                            Thread thread = new Thread(showform);
                            thread.IsBackground = false;
                            thread.Start();
                        }
                        else
                        {
                            MessageBox.Show("用户不存在！");
                            fw_exit(ihdev);
                        }

                    }
                }
            }
            else
            {
                timer1.Enabled = false;
                loginChoice error = new loginChoice();
                error.ShowDialog(this);
            }
        }
        void showform()
        {
            Application.Run(new _1_menuList(user, login_time));
        }
    }
}
