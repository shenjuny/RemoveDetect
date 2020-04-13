using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoveDetect
{
    public partial class userChange : Form
    {
        private config config;
        public userChange()
        {
            InitializeComponent();
        }
        public userChange(config config)
        {
            InitializeComponent();
            this.config = config;
        }
        private void btn_save_device_Click(object sender, EventArgs e)
        {
            try
            {
                user user = new user();
                user.name = txtUserName.Text.Trim();
                user.password = txtUserPassword.Text.Trim();
                user.IC = txtUserIC.Text.Trim();
                user.face = txtUserFace.Text.Trim();
                this.config.listUser.RemoveAt(this.config.num);
                this.config.listUser.Add(user);

                DataTable dataTable = Common.FillDataTable<user>(this.config.listUser);
                DBSet.dataTableToCsvT(dataTable, @"..\user.csv");

                MessageBox.Show("保存成功");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_cancel_device_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userChange_Load(object sender, EventArgs e)
        {
            this.txtUserName.Text = this.config.listUser[this.config.num].name;
            this.txtUserPassword.Text = this.config.listUser[this.config.num].password;
            this.txtUserIC.Text = this.config.listUser[this.config.num].IC;
            this.txtUserFace.Text = this.config.listUser[this.config.num].face;
        }
    }
}
