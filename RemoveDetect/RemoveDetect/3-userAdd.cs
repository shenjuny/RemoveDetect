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
    public partial class _3_userAdd : Form
    {
        public _3_userAdd()
        {
            InitializeComponent();
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

        private void save_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //表格新增
            //    //int index = this.dataGridViewUser.Rows.Add();
            //    //this.dataGridViewUser.Rows[index].Cells[0].Value = txtUserName.Text;
            //    //this.dataGridViewUser.Rows[index].Cells[1].Value = txtUserPassword.Text;
            //    //this.dataGridViewUser.Rows[index].Cells[2].Value = txtUserIC.Text;
            //    //this.dataGridViewUser.Rows[index].Cells[3].Value = txtUserFace.Text;
            //    //this.dataGridViewUser.Refresh();
            //    //数据库新增
            //    user user = new user();
            //    user.name = txtUserName.Text.Trim();
            //    user.password = txtUserPassword.Text.Trim();
            //    user.IC = txtUserIC.Text.Trim();
            //    user.face = txtUserFace.Text.Trim();
            //    if (check_user(user))
            //    {
            //        //List<user> listUser = new List<user>();
            //        listUser.Add(user);
            //        DataTable dataTable = Common.FillDataTable<user>(listUser);
            //        this.dataGridViewUser.DataSource = dataTable;
            //        DBSet.dataTableToCsvT(dataTable, @"..\user.csv");
            //        model t = new model();
            //        t.SetTrainedFaceRecognizer(model.FaceRecognizerType.LBPHFaceRecognizer);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            try
            {
                Service1 x = new Service1();
                //数据库新增
                var result = x.addUser(txtUserName.Text.Trim(), txtPassword.Text.Trim(), lblIC.Text.Trim(), "",txtPhone.Text.Trim(), txtNumber.Text.Trim(), txtGroup.Text.Trim(), txtDepartment.Text.Trim());
                //user user = new user();
                //user.name = txtName.Text.Trim();
                //user.password = txtPassword.Text.Trim();
                //user.IC = lblIC.Text.Trim();
                //user.phone = txtPhone.Text.Trim();
                //user.number = txtPhone.Text.Trim();
                //user.groupNum = txtGroup.Text.Trim();
                //user.department = txtDepartment.Text.Trim();
                //user.face = "";

                //if (check_user(user))
                //{
                //    //List<user> listUser = new List<user>();
                //    listUser.Add(user);
                //    DataTable dataTable = Common.FillDataTable<user>(listUser);
                //    this.dataGridViewUser.DataSource = dataTable;
                //    DBSet.dataTableToCsvT(dataTable, @"..\user.csv");
                //    model t = new model();
                //    t.SetTrainedFaceRecognizer(model.FaceRecognizerType.LBPHFaceRecognizer);
                //}
                if (result.success)
                {
                    //List<user> listUser = new List<user>();
                    //listUser.Add(user);
                    //DataTable dataTable = Common.FillDataTable<user>(listUser);
                    //uM.DataBindings;
                    this.Close();
                    Thread thread = new Thread(showform2);
                    thread.IsBackground = false;
                    thread.Start();
                }
                else
                {
                    MessageBox.Show(result.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void showform2()
        {
            Application.Run(new _3_userManage());
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread thread = new Thread(showform2);
            thread.IsBackground = false;
            thread.Start();
        }
    }
}
