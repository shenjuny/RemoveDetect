using RDService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace RemoveDetect
{
    public partial class logon : Form
    {
        public bool isCancel = true;
        public string login_time = "";
        public string user = "";
        public bool isok = false;
        public logon()
        {
            InitializeComponent();
        }
        private void btn_log_on_Click(object sender, EventArgs e)
        {
            //Service1 x = new Service1();
            //创建数据库连接类的对象
            SqlConnection con = new SqlConnection("server=.;database=RD;user=sa;pwd=Sa123456");
            //将连接打开
            con.Open();
            //执行con对象的函数，返回一个SqlCommand类型的对象
            SqlCommand cmd = con.CreateCommand();
            //把输入的数据拼接成sql语句，并交给cmd对象
            cmd.CommandText = "select * from [user] where name ='" + txtbox_user.Text.Trim() + "' and password = '"+txtbox_password.Text.Trim()+"'";
            //用cmd的函数执行语句，返回SqlDataReader对象dr,dr就是返回的结果集（也就是数据库中查询到的表数据）
            SqlDataReader dr = cmd.ExecuteReader();
            //用dr的read函数，每执行一次，返回一个包含下一行数据的集合dr，在执行read函数之前，dr并不是集合

            if (dr.Read())
            {
                //dr[]里面可以填列名或者索引，显示获得的数据</span>
                //MessageBox.Show(dr["Num"].ToString());
                login_time = DateTime.Now.ToString("");
                user = dr["id"].ToString();
                this.Close();
                Thread thread = new Thread(showform);
                thread.IsBackground = false;
                thread.Start();
            }
            else
            {
                MessageBox.Show("用户名或密码错误！请重新输入！");
            }
        }
        void showform()
        {
            Application.Run(new _1_menuList(user, login_time));
        }
               
        private void label4_Click(object sender, EventArgs e)
        {
            loginChoice lc = new loginChoice();
            lc.ShowDialog(this);
        }
    }
}
