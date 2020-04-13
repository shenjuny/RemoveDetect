using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RemoveDetect
{
    public partial class _6_InitConfig : Form
    {
        
        public _6_InitConfig()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 出厂初始化设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            //创建数据库连接类的对象
            SqlConnection con = new SqlConnection("server=.;database=RD;user=sa;pwd=Sa123456");
            //将连接打开
            con.Open();
            //执行con对象的函数，返回一个SqlCommand类型的对象
            SqlCommand cmd = con.CreateCommand();
            string Num = txtBox_Num.Text;
            string layer =txtBox_layer.Text;
            string row = txtBox_row.Text;
            string col = txtBox_col.Text;
            string type = txtBox_type.Text;
            string size = txtBox_size.Text;
            //把输入的数据拼接成sql语句，并交给cmd对象
            cmd.CommandText = "update device set Num='" + Num + "' , layer='" + layer + "' , row='" + row + "' , col='" + col + "' , type='" + type + "' , size='" + size + "' where id='1'";
            ////用cmd的函数执行语句，返回SqlDataReader对象dr,dr就是返回的结果集（也就是数据库中查询到的表数据）
            //SqlDataReader dr = cmd.ExecuteReader();
            ////用dr的read函数，每执行一次，返回一个包含下一行数据的集合dr，在执行read函数之前，dr并不是集合

            //if (dr.Read())
            //{
            //    //dr[]里面可以填列名或者索引，显示获得的数据</span>
            //    MessageBox.Show(dr[1].ToString());
            //}
            //增删改用ExecuteNonQuery，会返回一个整型数字
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                MessageBox.Show("修改成功");
            }
            else
            {
                MessageBox.Show("修改失败");
            }
            //用完后关闭连接，以免影响其他程序访问
            con.Close();
        }
        //添加数据
        private void button2_Click(object sender, EventArgs e)
        {
            string user = txtBox_type.Text;
            string pwd = txtBox_size.Text;
            //创建数据库连接类的对象
            SqlConnection con = new SqlConnection("server=.;database=data1220;user=sa;pwd=123");
            con.Open();
            //执行con对象的函数，返回一个SqlCommand类型的对象
            SqlCommand cmd = con.CreateCommand();
            //拼写语句
            cmd.CommandText = "insert into users values('" + user + "','" + pwd + "')";
            //增删改用ExecuteNonQuery，会返回一个整型数字
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {
                MessageBox.Show("添加成功");
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }
        private void _6_InitConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void _6_InitConfig_Load(object sender, EventArgs e)
        {
            
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
    }
}
