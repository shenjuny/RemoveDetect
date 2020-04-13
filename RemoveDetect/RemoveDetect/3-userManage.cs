using RDService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RemoveDetect
{
    public partial class _3_userManage : Form
    {

        public List<user> listUser = new List<user>();
        Service1 x = new Service1();
        public _3_userManage()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
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

        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread thread = new Thread(showform2);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform2()
        {
            Application.Run(new _3_userAdd());
        }

        private void _3_userManage_Load(object sender, EventArgs e)
        {
            var result = x.getUserList();
            for (int i =0;i<result.total;i++) {
                user user = new user();
                user.id = result.rows[i].id;
                user.IC = result.rows[i].IC;
                user.name = result.rows[i].name;
                user.department = result.rows[i].department;
                user.number = result.rows[i].number;
                user.password = result.rows[i].password;
                user.phone = result.rows[i].phone;
                user.groupNum = result.rows[i].groupNum;
                listUser.Add(user);
            }
            DataTable dataTable = Common.FillDataTable<user>(listUser);
            this.dataGridView1.DataSource = dataTable;
            listUser.Clear();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtBox_name.Text = "";
            txtBox_department.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var result = x.searchUsers(txtBox_name.Text.Trim(),txtBox_department.Text.Trim());
            for (int i = 0; i < result.total; i++)
            {
                user user = new user();
                user.IC = result.rows[i].IC;
                user.name = result.rows[i].name;
                user.number = result.rows[i].number;
                user.department = result.rows[i].department;
                user.password = result.rows[i].password;
                user.phone = result.rows[i].phone;
                user.groupNum = result.rows[i].groupNum;
                listUser.Add(user);
            }
            DataTable dataTable = Common.FillDataTable<user>(listUser);
            this.dataGridView1.DataSource = dataTable;

            listUser.Clear();
        }

        private void txtBox_name_TextChanged(object sender, EventArgs e)
        {
            
            var result = x.searchUsers(txtBox_name.Text.Trim(), txtBox_department.Text.Trim());
            for (int i = 0; i < result.total; i++)
            {
                user user = new user();
                user.IC = result.rows[i].IC;
                user.name = result.rows[i].name;
                user.number = result.rows[i].number;
                user.department = result.rows[i].department;
                user.password = result.rows[i].password;
                user.phone = result.rows[i].phone;
                user.groupNum = result.rows[i].groupNum;
                listUser.Add(user);
            }
            DataTable dataTable = Common.FillDataTable<user>(listUser);
            this.dataGridView1.DataSource = dataTable;
            listUser.Clear();
        }

        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            
            try
            {
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value != null)
                    {

                        if ((bool)dataGridView1.Rows[i].Cells[1].Value)
                        {
                            var result1 = x.deleteUser(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            if (result1.success)
                            {
                                var result = x.getUserList();
                                for (int j = 0; j < result.total; j++)
                                {
                                    user user = new user();
                                    user.id = result.rows[j].id;
                                    user.IC = result.rows[j].IC;
                                    user.department = result.rows[j].department;
                                    user.name = result.rows[j].name;
                                    user.number = result.rows[j].number;
                                    user.password = result.rows[j].password;
                                    user.phone = result.rows[j].phone;
                                    user.groupNum = result.rows[j].groupNum;
                                    listUser.Add(user);
                                }
                                DataTable dataTable = Common.FillDataTable<user>(listUser);
                                this.dataGridView1.DataSource = dataTable;
                                listUser.Clear();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUserManage_Click(object sender, EventArgs e)
        {
            btnUserManage.BackColor = SystemColors.HotTrack;
            btnUserManage.ForeColor = Color.White;
            btnRoleManage.BackColor = Color.White;
            btnRoleManage.ForeColor = Color.Black;
        }

        private void btnRoleManage_Click(object sender, EventArgs e)
        {
            //btnRoleManage.BackColor = SystemColors.HotTrack;
            //btnRoleManage.ForeColor = Color.White;
            //btnUserManage.BackColor = Color.White;
            //btnUserManage.ForeColor = Color.Black;

            this.Close();
            Thread thread = new Thread(showform3);
            thread.IsBackground = false;
            thread.Start();
        }
        void showform3()
        {
            Application.Run(new _3_userAuthority());
        }
    }
}
